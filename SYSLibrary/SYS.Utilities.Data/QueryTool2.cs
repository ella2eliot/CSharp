using SYS.Utilities.Configuration;
using SYS.Utilities.Security.Cryptography;
using SYS.Utilities.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SYS.Utilities.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryTool2 : QueryTool
    {
        private static readonly SerializableDictionary<string, ApplicationAccountSetting> _queryStringCache = new SerializableDictionary<string, ApplicationAccountSetting>();
        private static readonly object _lockObject = new object();
        private bool _mappingConnectionNameLoaded = false;

        /// <summary>
        /// 
        /// </summary>
        protected readonly QueryTool _sharedQueryTool;

        /// <summary>
        /// 
        /// </summary>
        public override string ConnectionName
        {
            get
            {
                if (this.DatabaseMapping != null)
                {
                    if (!_mappingConnectionNameLoaded)
                    {
                        lock (_lockObject)
                        {

                            if (!_mappingConnectionNameLoaded)
                            {
                                base.ConnectionName = ConfigManager.Instance.GetAppSetting(this.DatabaseMapping.AppConfigName, this.SpecificAssembly);
                                _mappingConnectionNameLoaded = true;
                            }
                        }
                    }
                }

                return base.ConnectionName;
            }
            protected set
            {
                base.ConnectionName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DbType
        {
            get
            {
                if (this.DatabaseMapping != null)
                {
                    base.DbType = this.DatabaseMapping.DbType;
                }

                return base.DbType;
            }
            protected set
            {
                base.DbType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <param name="sharedQueryTool"></param>
        internal QueryTool2(string connectionName, DatabaseMapping mapping, QueryTool sharedQueryTool)
            : this(connectionName, (Assembly)null, mapping, sharedQueryTool)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <param name="sharedQueryTool"></param>
        internal QueryTool2(string connectionName, Assembly specificAssembly, DatabaseMapping mapping, QueryTool sharedQueryTool)
            : base(connectionName, specificAssembly, mapping)
        {
            _sharedQueryTool = sharedQueryTool;
        }

        internal QueryTool2(string connectionName, string dbType, DatabaseMapping mapping, QueryTool sharedQueryTool)
            : this(connectionName, mapping, sharedQueryTool)
        {
            DbType = dbType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <param name="sharedQueryTool"></param>
        public QueryTool2(string connectionName, string dbType, Assembly specificAssembly, DatabaseMapping mapping, QueryTool sharedQueryTool)
            : this(connectionName, dbType, mapping, sharedQueryTool)
        {
            SpecificAssembly = specificAssembly;
        }

        /// <summary>
        /// 
        /// </summary>
        protected ApplicationAccountSetting CacheApplicationAccountSetting
        {
            get
            {
                var cacheName = string.Format("{0}-{1}", this.DbType, this.ConnectionName);

                if (_queryStringCache.ContainsKey(cacheName))
                {
                    lock (_lockObject)
                    {
                        if (_queryStringCache.ContainsKey(cacheName))
                        {
                            return _queryStringCache[cacheName];
                        }
                    }
                }

                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetConnection()
        {
            IDbConnection connection = null;
            var reallyConnectionName = string.Format("{0}-{1}", this.DbType, this.ConnectionName);

            var cacheAccount = CacheApplicationAccountSetting;

            if (cacheAccount != null)
            {
                try
                {
                    connection = this.GetConnection(cacheAccount);
                }
                catch (Exception)
                {
                    lock (_lockObject)
                    {
                        _queryStringCache.Remove(reallyConnectionName);
                    }
                }
            }

            // could not get collection instance, because connection open failed.
            if (connection == null)
            {
                var account = GetApplicationAccountSettingFromDatabase();

                connection = this.GetConnection(account);

                if (!_queryStringCache.ContainsKey(reallyConnectionName))
                {
                    lock (_lockObject)
                    {
                        if (!_queryStringCache.ContainsKey(reallyConnectionName))
                        {
                            _queryStringCache.Add(reallyConnectionName, account);
                        }
                    }
                }
            }

            return connection;
        }

        private ApplicationAccountSetting GetApplicationAccountSettingFromDatabase()
        {
            var reallyConnectionName = this.ConnectionName;
            var dbType = this.DbType;
            var sql = "select * from App_Account with (nolock) where AlisName = @AlisName and DbType = @DbType";
            var mappers = new List<SqlParameterMapper>
                                  {
                                      new SqlParameterMapper("@AlisName", reallyConnectionName),
                                      new SqlParameterMapper("@DbType", dbType),
                                  };

            using (var tx = new TransactionScope(TransactionScopeOption.Suppress))
            {
                var table = _sharedQueryTool.GetData(sql, mappers);

                if (table.Rows.Count == 0)
                {
                    throw new ApplicationException(string.Format("Could not find App_Account setting for AlisName ({0}) and DbType({1}).", reallyConnectionName, dbType));
                }

                var row = table.Rows[0];
                var account = new ApplicationAccountSetting
                {
                    DatabaseServer = Convert.ToString(row["DbServer"]),
                    DatabaseName = Convert.ToString(row["Dbname"]),
                    Login = Convert.ToString(row["LoginId"]).Trim(),
                    Password = Convert.ToString(row["LoginPwd"]).Trim(),
                };

                var sharedConnectionString = ConfigManager.Instance.GetConnectionString(_sharedQueryTool.ConnectionName, _sharedQueryTool.SpecificAssembly, _sharedQueryTool.DatabaseMapping);
                var sharedAppSetting = DataHelper.Instance.ConvertToObject(sharedConnectionString);

                account.ApplicationName = sharedAppSetting.ApplicationName;

                var encrypted = Convert.ToString(row["EncryptionFlag"]) == "1";
                var encryptKey = Convert.ToString(row["EncryptionKey"]).Trim();

                if (encrypted)
                {
                    IEncryption encryption = new TripleDESWrapper();

                    account.Login = encryption.DecryptData(account.Login, encryptKey);
                    account.Password = encryption.DecryptData(account.Password, encryptKey);
                }

                return account;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetConnectionString()
        {
            var reallyConnectionName = this.ConnectionName;
            var cacheAccount = CacheApplicationAccountSetting;

            if (cacheAccount != null)
            {
                return ConnectionHelper.GetConnectionString(cacheAccount);
            }

            var account = GetApplicationAccountSettingFromDatabase();
            var connectionString = ConnectionHelper.GetConnectionString(account);

            if (!_queryStringCache.ContainsKey(reallyConnectionName))
            {
                lock (_lockObject)
                {
                    if (!_queryStringCache.ContainsKey(reallyConnectionName))
                    {
                        _queryStringCache.Add(reallyConnectionName, account);
                    }
                }
            }

            return connectionString;
        }
    }
}

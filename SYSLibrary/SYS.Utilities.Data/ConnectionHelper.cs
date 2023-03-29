using SYS.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    public abstract class ConnectionHelper : IConnectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        protected ConnectionHelper()
        {
        }

        /// <summary>
        /// Create connection by default setting.
        /// connectionString name is 'default'.
        /// connection string stored in execute assembly.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return CreateConnection("default", (Assembly)null);
        }

        /// <summary>
        /// Create connection by specific name.
        /// connection string stored in execute assembly.
        /// </summary>
        /// <param name="connectionName">Connection name.</param>
        /// <returns></returns>
        public virtual IDbConnection CreateConnection(string connectionName)
        {
            return CreateConnection(connectionName, (Assembly)null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public virtual IDbConnection CreateConnection(string connectionName, DatabaseMapping mapping)
        {
            return this.CreateConnection(connectionName, null, mapping);
        }

        /// <summary>
        /// Create specific name connection.
        /// </summary>
        /// <param name="connectionName">Connection name.</param>
        /// <param name="specificAssembly"></param>
        /// <returns>Return opened connection.</returns>
        public virtual IDbConnection CreateConnection(string connectionName, Assembly specificAssembly)
        {
            if (specificAssembly == null)
            {
                return CreateConnectionFromExecuteAssembly(connectionName);
            }

            return CreateConnectionFromSpecificAssembly(connectionName, specificAssembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public virtual IDbConnection CreateConnection(string connectionName, Assembly specificAssembly, DatabaseMapping mapping)
        {
            if (specificAssembly == null)
            {
                return CreateConnectionFromExecuteAssembly(connectionName, mapping);
            }

            return CreateConnectionFromSpecificAssembly(connectionName, specificAssembly, mapping);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection(ApplicationAccountSetting accountSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract IDataParameter CreateParameter(SqlParameterMapper mapper);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public abstract SqlParameterMapper CreateParameterMapper(string parameterName, object value, string typeName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <returns></returns>
        protected abstract IDbConnection CreateConnectionFromSpecificAssembly(string connectionName, Assembly specificAssembly);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected abstract IDbConnection CreateConnectionFromSpecificAssembly(string connectionName, Assembly specificAssembly, DatabaseMapping mapping);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        protected abstract IDbConnection CreateConnectionFromExecuteAssembly(string connectionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected abstract IDbConnection CreateConnectionFromExecuteAssembly(string connectionName, DatabaseMapping mapping);

        /// <summary>
        /// Get database connection string.
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        public abstract string GetConnectionString(ApplicationAccountSetting accountSetting);

        internal ApplicationAccountSetting ConvertToObject(string connectionString)
        {
            var strings = connectionString.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var dic = new Dictionary<string, string>();

            foreach (var s in strings)
            {
                var split = s.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                var key = "";
                var value = "";

                if (split.Length > 0)
                {
                    key = split[0].ToUpper(CultureInfo.InvariantCulture).Trim();
                }

                if (split.Length > 1)
                {
                    value = split[1].ToUpper(CultureInfo.InvariantCulture).Trim();
                }

                dic.Add(key, value);
            }

            var setting = new ApplicationAccountSetting();
            var dicKey = ConnectionConstants.Oracle.DataSource.ToUpper(CultureInfo.InvariantCulture);

            setting.DatabaseServer = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.MSSQL.InitialCatalog.ToUpper(CultureInfo.InvariantCulture);
            setting.DatabaseName = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.Oracle.UserID.ToUpper(CultureInfo.InvariantCulture);
            setting.Login = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.Oracle.Password.ToUpper(CultureInfo.InvariantCulture);
            setting.Password = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            return setting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected virtual string GetDataType(string typeName)
        {

            if (typeName == DataTypes.Varchar)
            {
                return DataTypes.Varchar;
            }

            if (typeName == DataTypes.Nvarchar)
            {
                return DataTypes.Nvarchar;
            }

            if (typeName == DataTypes.DateTime)
            {
                return DataTypes.DateTime;
            }

            return DataTypes.Variant;
        }
    }
}

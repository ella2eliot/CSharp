using SYS.Utilities.Common;
using SYS.Utilities.Configuration;
using SYS.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    ///<summary>
	///</summary>
	public class QueryToolFactory
    {
        private readonly object _assemblyObject;
        private readonly string _connectionName;
        private readonly DatabaseMapping _databaseMapping;
        private readonly QueryToolFactory _shareQueryToolFactory;
        private readonly string _databaseType;
        private int _sqlCommandTimeout = 90;

        /// <summary>
        /// 
        /// </summary>
        public ILogging Logger { get; set; }

        /// <summary>
        /// SQL command time out.(second)
        /// </summary>
        public int SqlCommandTimeout
        {
            get { return _sqlCommandTimeout; }
            set { _sqlCommandTimeout = value; }
        }

        /// <summary>
        /// Database server type.
        /// </summary>
        public string DbType { get; private set; }

        ///<summary>
        /// Will use the 'default' connection string.
        ///</summary>
        public QueryToolFactory()
        {
            _connectionName = "default";
            _databaseType = DatabaseTypes.SQL;
            DbType = "DEFAULT";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        public QueryToolFactory(string connectionName)
            : this()
        {
            _connectionName = connectionName;
        }

        ///<summary>
        ///</summary>
        ///<param name="connectionName"></param>
        /// <param name="dbType"></param>
        public QueryToolFactory(string connectionName, string dbType)
            : this(connectionName)
        {
            DbType = dbType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, QueryToolFactory shareQueryToolFactory)
            : this(connectionName)
        {
            _shareQueryToolFactory = shareQueryToolFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="assemblyObject"></param>
        /// <param name="mapping"></param>
        public QueryToolFactory(string connectionName, object assemblyObject, DatabaseMapping mapping)
            : this(connectionName, assemblyObject)
        {
            _databaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="assemblyObject"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, object assemblyObject, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, assemblyObject)
        {
            _shareQueryToolFactory = shareQueryToolFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="assemblyObject"></param>
        /// <param name="mapping"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, object assemblyObject, DatabaseMapping mapping, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, assemblyObject, mapping)
        {
            _shareQueryToolFactory = shareQueryToolFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, string dbType, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, dbType)
        {
            _shareQueryToolFactory = shareQueryToolFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="shareQueryToolFactory"></param>
        /// <param name="databaseType">MS SQL or Oracle</param>
        public QueryToolFactory(string connectionName, string dbType, QueryToolFactory shareQueryToolFactory, string databaseType)
            : this(connectionName, dbType, shareQueryToolFactory)
        {
            _databaseType = databaseType;
        }

        ///<summary>
        ///</summary>
        ///<param name="connectionName"></param>
        ///<param name="dbType"> </param>
        ///<param name="databaseType"></param>
        public QueryToolFactory(string connectionName, string dbType, string databaseType)
            : this(connectionName, dbType)
        {
            _databaseType = databaseType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        public QueryToolFactory(string connectionName, DatabaseMapping mapping)
            : this(connectionName)
        {
            _databaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="assemblyObject"></param>
        public QueryToolFactory(string connectionName, object assemblyObject)
            : this(connectionName)
        {
            _assemblyObject = assemblyObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="assemblyObject"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject)
            : this(connectionName, dbType)
        {
            _assemblyObject = assemblyObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="assemblyObject"></param>
        /// <param name="databaseType"></param>
        public QueryToolFactory(string connectionName, object assemblyObject, string databaseType)
            : this(connectionName, assemblyObject)
        {
            _databaseType = databaseType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, DatabaseMapping mapping, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, shareQueryToolFactory)
        {
            _databaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="mapping"></param>
        public QueryToolFactory(string connectionName, string dbType, DatabaseMapping mapping)
            : this(connectionName, dbType)
        {
            _databaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="mapping"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, string dbType, DatabaseMapping mapping, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, dbType, null, mapping, shareQueryToolFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="assemblyObject"></param>
        /// <param name="mapping"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject, DatabaseMapping mapping)
            : this(connectionName, dbType, assemblyObject)
        {
            _databaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="assemblyObject"></param>
        /// <param name="mapping"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject, DatabaseMapping mapping, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, dbType, assemblyObject, mapping)
        {
            _shareQueryToolFactory = shareQueryToolFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="assemblyObject"></param>
        /// <param name="shareQueryToolFactory"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject, QueryToolFactory shareQueryToolFactory)
            : this(connectionName, dbType, assemblyObject, null, shareQueryToolFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="assemblyObject"></param>
        /// <param name="shareQueryToolFactory"></param>
        /// <param name="databaseType"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject, QueryToolFactory shareQueryToolFactory, string databaseType)
            : this(connectionName, dbType, assemblyObject, shareQueryToolFactory)
        {
            _databaseType = databaseType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"> </param>
        /// <param name="assemblyObject"></param>
        /// <param name="databaseType"></param>
        public QueryToolFactory(string connectionName, string dbType, object assemblyObject, string databaseType)
            : this(connectionName, dbType, assemblyObject)
        {
            _databaseType = databaseType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateInstanceEx<T>() where T : class, IQueryTool
        {
            T queryTool;
            IQueryTool sharedQueryTool = null;

            if (_shareQueryToolFactory != null)
            {
                sharedQueryTool = _shareQueryToolFactory.CreateInstance();
            }

            Assembly specificAssembly = null;

            if (_assemblyObject == null || _assemblyObject is NullObject)
            {
                specificAssembly = null;
            }
            else
            {
                specificAssembly = _assemblyObject.GetType().Assembly;
            }

            queryTool = (T)Activator.CreateInstance(typeof(T), new object[] { _connectionName, DbType, specificAssembly, _databaseMapping, sharedQueryTool });

            switch (_databaseType)
            {
                case DatabaseTypes.SQL:
                    queryTool.ConnectionHelper = new SqlConnectionHelper();
                    break;
                //case DatabaseTypes.SQL2:
                //    queryTool.ConnectionHelper = new SqlConnectionHelper2();
                //    break;
                //case DatabaseTypes.Oracle:
                //    queryTool.ConnectionHelper = new OracleConnectionHelper();
                //    break;
                //case DatabaseTypes.Oracle2:
                //    queryTool.ConnectionHelper = new OracleConnectionHelper2();
                //    break;
                default:
                    throw new IndexOutOfRangeException("Unsupport database type for '" + _databaseType + "'.");
            }

            queryTool.SqlCommandTimeout = this.SqlCommandTimeout;
            queryTool.Logger = this.Logger;

            return queryTool;
        }

        ///<summary>
        ///</summary>
        ///<returns></returns>
        public QueryTool CreateInstance()
        {
            QueryTool queryTool;
            QueryTool sharedQueryTool = null;

            if (_shareQueryToolFactory != null)
            {
                sharedQueryTool = _shareQueryToolFactory.CreateInstance();
            }

            if (_assemblyObject == null || _assemblyObject is NullObject)
            {
                queryTool = sharedQueryTool == null ? new QueryTool(_connectionName, DbType, _databaseMapping)
                    : new QueryTool2(_connectionName, DbType, (Assembly)null, _databaseMapping, sharedQueryTool);
            }
            else
            {
                var specificAssembly = _assemblyObject.GetType().Assembly;

                queryTool = sharedQueryTool == null ? new QueryTool(_connectionName, DbType, specificAssembly, _databaseMapping)
                    : new QueryTool2(_connectionName, DbType, specificAssembly, _databaseMapping, sharedQueryTool);
            }

            switch (_databaseType)
            {
                case DatabaseTypes.SQL:
                    queryTool.ConnectionHelper = new SqlConnectionHelper();
                    break;
                //case DatabaseTypes.SQL2:
                //    queryTool.ConnectionHelper = new SqlConnectionHelper2();
                //    break;
                //case DatabaseTypes.Oracle:
                //    queryTool.ConnectionHelper = new OracleConnectionHelper();
                //    break;
                //case DatabaseTypes.Oracle2:
                //    queryTool.ConnectionHelper = new OracleConnectionHelper2();
                //    break;
                default:
                    throw new IndexOutOfRangeException("Unsupport database type for '" + _databaseType + "'.");
            }

            queryTool.SqlCommandTimeout = this.SqlCommandTimeout;
            queryTool.Logger = this.Logger;
            return queryTool;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SYS.Utilities.Configuration;

namespace SYS.Utilities.Data
{
    ///<summary>
	///</summary>
	public class SqlConnectionHelper : ConnectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        public override IDbConnection CreateConnection(ApplicationAccountSetting accountSetting)
        {
            var connectionString = GetConnectionString(accountSetting);

            return GetConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public override IDataParameter CreateParameter(SqlParameterMapper mapper)
        {
            var sqlParameter = new SqlParameter(mapper.Name, mapper.Value);

            if (mapper.DataType != DataTypes.Variant)
            {
                var dbType = this.GetDbType(mapper.DataType);

                sqlParameter.SqlDbType = dbType;
            }


            return sqlParameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public override SqlParameterMapper CreateParameterMapper(string parameterName, object value, string typeName)
        {
            var mapper = new SqlParameterMapper(parameterName, value);

            if (!string.IsNullOrWhiteSpace(typeName))
            {
                mapper.DataType = this.GetDataType(typeName);
            }

            return mapper;
        }

        private SqlDbType GetDbType(string dataType)
        {
            if (dataType == DataTypes.Varchar)
            {
                return SqlDbType.VarChar;
            }

            if (dataType == DataTypes.Nvarchar)
            {
                return SqlDbType.NVarChar;
            }

            return SqlDbType.Variant;
        }

        /// <summary>
        /// Get database connection string.
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        public override string GetConnectionString(ApplicationAccountSetting accountSetting)
        {
            var connectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True",
                accountSetting.DatabaseServer, accountSetting.DatabaseName, accountSetting.Login, accountSetting.Password);

            if (!string.IsNullOrWhiteSpace(accountSetting.ApplicationName))
            {
                connectionString += ";" + string.Format("Application Name={0};", accountSetting.ApplicationName);
            }

            return connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <returns></returns>
        protected override IDbConnection CreateConnectionFromSpecificAssembly(string connectionName, Assembly specificAssembly)
        {
            var connectionString = ConfigManager.Instance.GetConnectionString(connectionName, specificAssembly);

            return GetConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected override IDbConnection CreateConnectionFromSpecificAssembly(string connectionName, Assembly specificAssembly, DatabaseMapping mapping)
        {
            var connectionString = ConfigManager.Instance.GetConnectionString(connectionName, specificAssembly, mapping);

            return GetConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        protected override IDbConnection CreateConnectionFromExecuteAssembly(string connectionName)
        {
            var connectionString = ConfigManager.Instance.GetConnectionString(connectionName);

            return GetConnection(connectionString);
        }

        private IDbConnection GetConnection(string connectionString)
        {
            var connection = (IDbConnection)new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                var setting = ConvertToObject(connectionString);
                var message = string.Format("Connect to database server '{0}' failed.", setting.DatabaseServer);

                throw new ApplicationException(message, ex);
            }

            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        protected override IDbConnection CreateConnectionFromExecuteAssembly(string connectionName, DatabaseMapping mapping)
        {
            var connectionString = ConfigManager.Instance.GetConnectionString(connectionName, mapping);

            return GetConnection(connectionString);
        }
    }
}

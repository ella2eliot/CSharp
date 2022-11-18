using SYS.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    public interface IConnectionHelper
    {
        /// <summary>
		/// Create connection by default setting.
		/// connectionString name is 'default'.
		/// connection string stored in execute assembly.
		/// </summary>
		/// <returns></returns>
		IDbConnection CreateConnection();

        /// <summary>
        /// Create connection by specific name.
        /// connection string stored in execute assembly.
        /// </summary>
        /// <param name="connectionName">Connection name.</param>
        /// <returns></returns>
        IDbConnection CreateConnection(string connectionName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        IDbConnection CreateConnection(string connectionName, DatabaseMapping mapping);

        /// <summary>
        /// Create specific name connection.
        /// </summary>
        /// <param name="connectionName">Connection name.</param>
        /// <param name="specificAssembly"></param>
        /// <returns>Return opened connection.</returns>
        IDbConnection CreateConnection(string connectionName, Assembly specificAssembly);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        IDbConnection CreateConnection(string connectionName, Assembly specificAssembly, DatabaseMapping mapping);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        IDbConnection CreateConnection(ApplicationAccountSetting accountSetting);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        IDataParameter CreateParameter(SqlParameterMapper mapper);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        SqlParameterMapper CreateParameterMapper(string parameterName, object value, string typeName);

        /// <summary>
        /// Get database connection string.
        /// </summary>
        /// <param name="accountSetting"></param>
        /// <returns></returns>
        string GetConnectionString(ApplicationAccountSetting accountSetting);
    }
}

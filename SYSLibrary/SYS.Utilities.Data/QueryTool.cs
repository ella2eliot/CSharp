using SYS.Utilities.Configuration;
using SYS.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class QueryTool : IQueryTool
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogging Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IConnectionHelper ConnectionHelper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ConnectionName { get; protected set; }

        /// <summary>
        /// 資料庫類別.
        /// </summary>
        public virtual String DbType { get; protected set; }

        /// <summary>
        /// SQL command execute time out.(second)
        /// </summary>
        public int SqlCommandTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DatabaseMapping DatabaseMapping { get; protected set; }

        ///<summary>
        ///</summary>
        public Assembly SpecificAssembly { get; protected set; }

        /// <summary>
        /// Create default connection named 'default', and stored in execute assembly.
        /// </summary>
        internal QueryTool()
            : this("default", (Assembly)null)
        {
        }

        /// <summary>
        /// Create connection by specific name, and stored in execute assembly.
        /// </summary>
        /// <param name="connectionName">Connection string name in config.</param>
        internal QueryTool(string connectionName)
            : this(connectionName, (Assembly)null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        internal QueryTool(string connectionName, DatabaseMapping mapping)
            : this(connectionName)
        {
            DatabaseMapping = mapping;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="mapping"></param>
        internal QueryTool(string connectionName, string dbType, DatabaseMapping mapping)
            : this(connectionName, mapping)
        {
            DbType = dbType;
        }

        /// <summary>
        /// Create connection by specific name, and stored in specific assembly.
        /// </summary>
        /// <param name="connectionName">Connection string name in config.</param>
        /// <param name="specificAssembly">Connection string stored in assembly</param>
        internal QueryTool(string connectionName, Assembly specificAssembly)
        {
            this.ConnectionName = connectionName;
            this.SpecificAssembly = specificAssembly;
            this.SqlCommandTimeout = 90;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        internal QueryTool(string connectionName, Assembly specificAssembly, DatabaseMapping mapping)
            : this(connectionName, specificAssembly)
        {
            this.DatabaseMapping = mapping;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="dbType"></param>
        /// <param name="specificAssembly"></param>
        /// <param name="mapping"></param>
        internal QueryTool(string connectionName, string dbType, Assembly specificAssembly, DatabaseMapping mapping)
            : this(connectionName, specificAssembly, mapping)
        {
            DbType = dbType;
        }

        ///<summary>
        ///</summary>
        ///<param name="parameterName"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        public virtual SqlParameterMapper CreateParameterMapper(string parameterName, object value)
        {
            parameterName = this.GetParameterPreFix() + parameterName;

            return new SqlParameterMapper(parameterName, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual int ExecuteNoneQuery(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction)
        {
            if (transaction == null)
            {
                return ExecuteNoneQuery(sql, mappers);
            }

            return ExecuteNoneQueryWithTransaction(sql, mappers, transaction);
        }

        private int ExecuteNoneQueryWithTransaction(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction)
        {
            var commandText = this.RemoveNoLockStatement(sql);
            var cmd = transaction.Connection.CreateCommand();

            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandType = GetCommandType(commandText);
            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            this.WriteLog(cmd.CommandText);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(transaction.Connection.ConnectionString, ex);

                throw failedException;
            }
        }

        ApplicationException GetExecuteCommandFailedException(string connectionString, Exception ex)
        {
            var setting = ((ConnectionHelper)this.ConnectionHelper).ConvertToObject(connectionString);
            var message = string.Format("Execute command failed on database '{0}..{1}'.", setting.DatabaseServer, setting.DatabaseName);

            return new ApplicationException(message, ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        public virtual int ExecuteNoneQuery(string sql, List<SqlParameterMapper> mappers)
        {
            var commandText = sql;

            using (var connection = this.GetConnection())
            {
                var cmd = connection.CreateCommand();

                cmd.Connection = connection;
                cmd.CommandType = GetCommandType(commandText);

                if (Transaction.Current != null)
                {
                    commandText = this.RemoveNoLockStatement(commandText);
                }

                cmd.CommandText = commandText;
                cmd.CommandTimeout = this.SqlCommandTimeout;

                if (mappers != null)
                {
                    foreach (var p in mappers)
                    {
                        var parameter = this.ConnectionHelper.CreateParameter(p);

                        cmd.Parameters.Add(parameter);
                    }
                }

                this.WriteLog(cmd.CommandText);

                var executeNoneQuery = cmd.ExecuteNonQuery();

                if (mappers != null)
                {
                    foreach (var mapper in mappers)
                    {
                        if (mapper.Direction == SqlParameterDirection.Output)
                        {
                            var value = (IDataParameter)cmd.Parameters[mapper.Name];
                            mapper.Value = value.Value;
                        }
                    }
                }

                try
                {
                    return executeNoneQuery;
                }
                catch (Exception ex)
                {
                    var failedException = GetExecuteCommandFailedException(connection.ConnectionString, ex);

                    throw failedException;
                }
            }
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<returns></returns>
        public virtual int ExecuteNoneQuery(string sql)
        {
            return ExecuteNoneQuery(sql, null);
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<returns></returns>
        public virtual CommandType GetCommandType(string sql)
        {
            if (Regex.IsMatch(sql, "(select\\s)|(insert\\s)|(update\\s)|(delete\\s)|(create\\s+table)|(drop\\s+table)|(truncate\\s+table)",
                              RegexOptions.IgnoreCase))
            {
                return CommandType.Text;
            }

            return CommandType.StoredProcedure;
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<param name="mappers"></param>
        ///<param name="transaction"></param>
        ///<returns></returns>
        public virtual DataTable GetData(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction)
        {
            if (transaction == null)
            {
                return GetData(sql, mappers);
            }

            var commandText = this.RemoveNoLockStatement(sql);
            var cmd = transaction.Connection.CreateCommand();

            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandType = GetCommandType(commandText);
            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            IDataAdapter adapter;

            if (this.ConnectionHelper is SqlConnectionHelper)
            {
                adapter = new SqlDataAdapter((SqlCommand)cmd);
            }
            else
            {
                adapter = new OracleDataAdapter((OracleCommand)cmd);
            }

            var ds = new DataSet();

            this.WriteLog(cmd.CommandText);

            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(transaction.Connection.ConnectionString, ex);

                throw failedException;
            }

            return ds.Tables[0];
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<param name="mappers"></param>
        ///<param name="connection"></param>
        ///<returns></returns>
        public virtual DataTable GetData(string sql, List<SqlParameterMapper> mappers, IDbConnection connection)
        {
            var commandText = sql;
            var cmd = connection.CreateCommand();

            cmd.Connection = connection;
            cmd.CommandType = GetCommandType(sql);

            if (Transaction.Current != null)
            {
                commandText = this.RemoveNoLockStatement(commandText);
            }

            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            IDataAdapter adapter;

            if (this.ConnectionHelper is SqlConnectionHelper)
            {
                adapter = new SqlDataAdapter((SqlCommand)cmd);
            }
            else
            {
                adapter = new OracleDataAdapter((OracleCommand)cmd);
            }

            DataSet ds = new DataSet();

            this.WriteLog(cmd.CommandText);

            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(connection.ConnectionString, ex);

                throw failedException;
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string sql, List<SqlParameterMapper> mappers, IDbConnection connection)
        {
            var commandText = sql;
            var cmd = connection.CreateCommand();

            cmd.Connection = connection;
            cmd.CommandType = GetCommandType(sql);

            if (Transaction.Current != null)
            {
                commandText = this.RemoveNoLockStatement(commandText);
            }

            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            IDataAdapter adapter;

            if (this.ConnectionHelper is SqlConnectionHelper)
            {
                adapter = new SqlDataAdapter((SqlCommand)cmd);
            }
            else
            {
                adapter = new OracleDataAdapter((OracleCommand)cmd);
            }

            DataSet ds = new DataSet();

            this.WriteLog(cmd.CommandText);

            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(connection.ConnectionString, ex);

                throw failedException;
            }

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection GetConnection()
        {
            return ConnectionHelper.CreateConnection(ConnectionName, SpecificAssembly, this.DatabaseMapping);
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<param name="mappers"></param>
        ///<returns></returns>
        public virtual DataTable GetData(string sql, List<SqlParameterMapper> mappers)
        {
            using (var connection = this.GetConnection())
            {
                return GetData(sql, mappers, connection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string sql, List<SqlParameterMapper> mappers)
        {
            using (var connection = this.GetConnection())
            {
                return GetDataSet(sql, mappers, connection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataTable GetData(string sql)
        {
            var mappers = new List<SqlParameterMapper>();

            return GetData(sql, mappers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual DataSet GetDataSet(string sql)
        {
            var mappers = new List<SqlParameterMapper>();

            return GetDataSet(sql, mappers);
        }

        ///<summary>
        ///</summary>
        ///<param name="sql"></param>
        ///<param name="mappers"></param>
        ///<param name="transaction"></param>
        ///<returns></returns>
        public virtual object ExecuteScalar(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction)
        {
            if (transaction == null)
            {
                return ExecuteScalar(sql, mappers);
            }

            return ExecuteScalarWithTransaction(sql, mappers, transaction);
        }

        private object ExecuteScalarWithTransaction(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction)
        {
            var commandText = this.RemoveNoLockStatement(sql);
            var cmd = transaction.Connection.CreateCommand();

            cmd.Connection = transaction.Connection;
            cmd.Transaction = transaction;
            cmd.CommandType = GetCommandType(commandText);
            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            this.WriteLog(cmd.CommandText);

            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(transaction.Connection.ConnectionString, ex);

                throw failedException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, List<SqlParameterMapper> mappers)
        {
            var commandText = sql;

            using (var connection = this.GetConnection())
            {
                var cmd = connection.CreateCommand();

                cmd.Connection = connection;
                cmd.CommandType = GetCommandType(commandText);

                if (Transaction.Current != null)
                {
                    commandText = this.RemoveNoLockStatement(commandText);
                }

                cmd.CommandText = commandText;
                cmd.CommandTimeout = this.SqlCommandTimeout;

                if (mappers != null)
                {
                    foreach (var p in mappers)
                    {
                        var parameter = this.ConnectionHelper.CreateParameter(p);

                        cmd.Parameters.Add(parameter);
                    }
                }

                this.WriteLog(cmd.CommandText);

                try
                {
                    return cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    var failedException = GetExecuteCommandFailedException(connection.ConnectionString, ex);

                    throw failedException;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null, (IDbTransaction)null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, IDbTransaction transaction)
        {
            return ExecuteScalar(sql, null, transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual object ExecuteScalar(string sql, List<SqlParameterMapper> mappers, IDbConnection connection)
        {
            var commandText = sql;
            var cmd = connection.CreateCommand();

            cmd.Connection = connection;
            cmd.CommandType = GetCommandType(commandText);

            if (Transaction.Current != null)
            {
                commandText = this.RemoveNoLockStatement(commandText);
            }

            cmd.CommandText = commandText;
            cmd.CommandTimeout = this.SqlCommandTimeout;

            if (mappers != null)
            {
                foreach (var p in mappers)
                {
                    var parameter = this.ConnectionHelper.CreateParameter(p);

                    cmd.Parameters.Add(parameter);
                }
            }

            this.WriteLog(cmd.CommandText);

            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                var failedException = GetExecuteCommandFailedException(connection.ConnectionString, ex);

                throw failedException;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        public virtual List<SqlParameterMapper> FillParameter<T>(T item, params List<string>[] fieldLists) where T : class
        {
            if (item == null)
            {
                throw new ArgumentException("Could not be null or empty.", "item");
            }

            if (fieldLists == null || fieldLists.Length == 0)
            {
                throw new ArgumentException("Could not be null or empty.", "fieldLists");
            }

            var results = new List<SqlParameterMapper>();

            foreach (var lists in fieldLists)
            {
                var cfields = new List<FieldCondition>();

                foreach (var list in lists)
                {
                    cfields.Add(new FieldCondition(list));
                }

                results.AddRange(FillParameter(item, cfields));
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        public virtual List<SqlParameterMapper> FillParameter<T>(T item, params List<FieldCondition>[] fieldLists) where T : class
        {
            var fields = new List<string>();

            foreach (var conditions in fieldLists)
            {
                foreach (var field in conditions)
                {
                    fields.Add(field.FieldName);
                }
            }

            return this.FillParameter(item, fields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="valueFields"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        public virtual List<SqlParameterMapper> FillParameter<T>(T item, List<string> valueFields, params List<FieldCondition>[] fieldLists) where T : class
        {
            var fields = new List<FieldCondition>();

            foreach (var valueField in valueFields)
            {
                fields.Add(new FieldCondition(valueField));
            }

            foreach (var conditions in fieldLists)
            {
                foreach (var field in conditions)
                {
                    fields.Add(field);
                }
            }

            return this.FillParameter(item, fields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        public virtual List<SqlParameterMapper> FillParameter<T>(T item, List<FieldCondition> fieldLists) where T : class
        {
            if (item == null)
            {
                throw new ArgumentException("Could not be null or empty.", "item");
            }

            if (fieldLists == null || fieldLists.Count == 0)
            {
                throw new ArgumentException("Could not be null or empty.", "fieldLists");
            }

            var results = new List<SqlParameterMapper>();
            var itemType = item.GetType();
            var propertyInfos = itemType.GetProperties();

            foreach (var field in fieldLists)
            {
                if (field.ArgumentValue != null)
                {
                    var mapper = this.ConnectionHelper.CreateParameterMapper(this.GetParameterPreFix() + field.ArgumentName, field.ArgumentValue, field.DataType);

                    results.Add(mapper);

                    continue;
                }

                foreach (var property in propertyInfos)
                {
                    var attributes = property.GetCustomAttributes(true);

                    foreach (var attribute in attributes)
                    {
                        var fieldAttribute = attribute as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

                        if (fieldAttribute != null)
                        {
                            if (fieldAttribute.Name == field.FieldName)
                            {
                                var value = itemType.InvokeMember(property.Name, BindingFlags.GetProperty, null, item, null) ?? DBNull.Value;
                                var parameter = this.ConnectionHelper.CreateParameterMapper(this.GetParameterPreFix() + field.ArgumentName, value, fieldAttribute.TypeName);

                                results.Add(parameter);

                                break;
                            }
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public virtual DataTable GetData(string sql, IDbTransaction transaction)
        {
            return GetData(sql, null, transaction);
        }

        private string GetParameterPreFix()
        {
            if (this.ConnectionHelper is SqlConnectionHelper)
            {
                return "@";
            }

            return ":";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        protected IDbConnection GetConnection(ApplicationAccountSetting account)
        {
            return this.ConnectionHelper.CreateConnection(account);
        }

        /// <summary>【
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetConnectionString()
        {
            throw new NotImplementedException();
        }

        private string RemoveNoLockStatement(string sql)
        {
            return sql.Replace("with (nolock)", "");//.Replace("(nolock)", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected void WriteLog(string message)
        {
            if (this.Logger != null)
            {
                this.Logger.WriteMessage(message);
            }
        }
    }
}

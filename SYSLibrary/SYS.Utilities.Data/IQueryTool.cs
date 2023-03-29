using SYS.Utilities.Configuration;
using SYS.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQueryTool
    {
        /// <summary>
        /// 
        /// </summary>
        ILogging Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IConnectionHelper ConnectionHelper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ConnectionName { get; }

        /// <summary>
        /// 資料庫類別.
        /// </summary>
        string DbType { get; }

        /// <summary>
        /// SQL command execute time out.(second)
        /// </summary>
        int SqlCommandTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DatabaseMapping DatabaseMapping { get; }

        ///<summary>
        ///</summary>
        Assembly SpecificAssembly { get; }

        ///<summary>
        ///</summary>
        ///<param name="parameterName"></param>
        ///<param name="value"></param>
        ///<returns></returns>
        SqlParameterMapper CreateParameterMapper(string parameterName, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteNoneQuery(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        int ExecuteNoneQuery(string sql, List<SqlParameterMapper> mappers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int ExecuteNoneQuery(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, List<SqlParameterMapper> mappers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, List<SqlParameterMapper> mappers, IDbConnection connection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        List<SqlParameterMapper> FillParameter<T>(T item, params List<string>[] fieldLists) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        List<SqlParameterMapper> FillParameter<T>(T item, params List<FieldCondition>[] fieldLists) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="valueFields"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        List<SqlParameterMapper> FillParameter<T>(T item, List<string> valueFields, params List<FieldCondition>[] fieldLists) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="fieldLists"></param>
        /// <returns></returns>
        List<SqlParameterMapper> FillParameter<T>(T item, List<FieldCondition> fieldLists) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetData(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        DataTable GetData(string sql, IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        DataTable GetData(string sql, List<SqlParameterMapper> mappers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        DataTable GetData(string sql, List<SqlParameterMapper> mappers, IDbConnection connection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        DataTable GetData(string sql, List<SqlParameterMapper> mappers, IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, List<SqlParameterMapper> mappers);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mappers"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, List<SqlParameterMapper> mappers, IDbConnection connection);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    public class QueryToolObject
    {
        public string ConnectionSetting { get; set; }

        public QueryToolObject(string connectionSetting)
        {
            this.ConnectionSetting = connectionSetting;
        }

        public QueryToolObject(string dbServer, string dbname, string loginId, string loginPwd)
        {
            this.ConnectionSetting = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}"
                , dbServer, dbname, loginId, loginPwd);
        }

        public DataTable GetQueryData(string sqlCommandText)
        {
            var connectionString = ConnectionSetting;
            var sql = sqlCommandText;

            var result = new DataTable();
            var connection = (IDbConnection)new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 90;

                IDataAdapter adapter;
                adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                result = ds.Tables[0];
            }

            return result;
        }

        public int ExecuteNonQuery(string sqlCommandText)
        {
            var connectionString = ConnectionSetting;
            var sql = sqlCommandText;

            var connection = (IDbConnection)new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 90;

                return cmd.ExecuteNonQuery();
            }
        }
        public int ExecuteNonQuery(string spName, List<StoredProcedureParams> spParams)
        {
            var result = 0;
            var connection = (IDbConnection)new SqlConnection(ConnectionSetting);
            using (connection)
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;  // 設定執行預先寫好的SP
                cmd.CommandText = spName;                       // 要使用的SP名稱
                cmd.CommandTimeout = 90;                        // 處理的Time Out 時間 (單位: 秒)


                foreach (var spParam in spParams)
                {
                    var parameter1 = cmd.CreateParameter();
                    parameter1.ParameterName = String.Format("@{0}", spParam.Name);
                    parameter1.Value = spParam.Value;
                    cmd.Parameters.Add(parameter1);
                }

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public DataTable GetQueryData(string spName, List<StoredProcedureParams> spParams)
        {
            var result = new DataTable();
            var connection = (IDbConnection)new SqlConnection(ConnectionSetting);
            using (connection)
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.StoredProcedure;  // 設定執行預先寫好的SP
                cmd.CommandText = spName;                       // 要使用的SP名稱
                cmd.CommandTimeout = 90;                        // 處理的Time Out 時間 (單位: 秒)

                foreach (var spParam in spParams)
                {
                    var parameter1 = cmd.CreateParameter();
                    parameter1.ParameterName = String.Format("@{0}", spParam.Name);
                    parameter1.Value = spParam.Value;
                    cmd.Parameters.Add(parameter1);
                }

                IDataAdapter adapter;
                adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                result = ds.Tables[0];
            }
            return result;
        }

        public DataTable GetQueryData(string tableName, List<IndexParams> selectParams)
        {
            var sqlHelper = new SQLHelper();
            var cfs = new List<string>();
            foreach (var selectParam in selectParams)
            {
                cfs.Add(selectParam.Name);
            }

            var sql = sqlHelper.GetSelectSQL(tableName, null, cfs);

            var connectionString = ConnectionSetting;
            //var sql = sqlCommandText;

            var result = new DataTable();
            var connection = (IDbConnection)new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.CommandTimeout = 90;

                foreach (var spParam in selectParams)
                {
                    var parameter1 = cmd.CreateParameter();
                    parameter1.ParameterName = String.Format("@{0}", spParam.Name);
                    parameter1.Value = spParam.Value;
                    cmd.Parameters.Add(parameter1);
                }

                IDataAdapter adapter;
                adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet ds = new DataSet();

                adapter.Fill(ds);

                result = ds.Tables[0];
            }

            return result;
        }


    }
}

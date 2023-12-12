using System;
using System.Data;
using System.Linq;

namespace CSharpConsole
{
    public static class DataSetFunctions
    {
        public static void BuildInserSQL()
        {
            DataSet dataSet = GetSampleDataSet();

            // 遍歷每個 DataTable
            foreach (DataTable table in dataSet.Tables)
            {
                // 遍歷每個 DataRow
                foreach (DataRow row in table.Rows)
                {
                    // 指定要插入的欄位名稱
                    var columnNames = new[] { "Emp_name", "Emp_no", "Dept" };

                    // 組成 INSERT 語句
                    string insertSql = GenerateInsertSql(table.TableName, row, columnNames);
                    Console.WriteLine(insertSql);
                }
            }
        }
        static DataSet GetSampleDataSet()
        {
            // 假設這是您的 DataSet，包含一個名為 "Table1" 的 DataTable
            DataSet dataSet = new DataSet();

            DataTable table = new DataTable("Table1");
            table.Columns.Add("Emp_name", typeof(string));
            table.Columns.Add("Emp_no", typeof(int));
            table.Columns.Add("Dept", typeof(string));

            table.Rows.Add("John Doe", 101, "HR");
            table.Rows.Add("Jane Doe", 102, "IT");

            dataSet.Tables.Add(table);

            return dataSet;
        }
        static string GenerateInsertSql(string tableName, DataRow row, string[] columnNames)
        {
            // 組成 INSERT 語句
            string insertSql = $"INSERT INTO {tableName} (";

            // 添加指定的列名
            insertSql += string.Join(", ", columnNames);

            // 添加值
            insertSql += ") VALUES (";
            insertSql += string.Join(", ", columnNames.Select(col => $"\'{row[col]}\'")); // 假設所有值都是字串型態

            insertSql += ");";

            return insertSql;
        }
    }
}

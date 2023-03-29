using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class SQLHelper
    {
        private readonly string _parameterPreFix = "@";

        /// <summary>
        /// 
        /// </summary>
        public string DatabaseType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public SQLHelper()
        {
            this.DatabaseType = DatabaseTypes.SQL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseType"></param>
        public SQLHelper(string databaseType)
        {
            this.DatabaseType = databaseType;

            if (databaseType == DatabaseTypes.Oracle)
            {
                _parameterPreFix = ":";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, null, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, bool distinct)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, null, distinct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="tops"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, int tops, List<OrderBy> orderBys)
        {
            return GetSelectSQL(tableName, valueFields, conditionFields, tops, orderBys, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="tops"></param>
        /// <param name="orderBys"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, int tops, List<OrderBy> orderBys, bool distinct)
        {
            var cfields = this.ConvertFieldCondition(conditionFields);

            return this.GetSelectSQL(tableName, valueFields, cfields, tops, orderBys, distinct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="orderBys"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, List<OrderBy> orderBys, bool distinct)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, orderBys, distinct);
        }

        private List<FieldCondition> ConvertFieldCondition(List<string> conditionFields)
        {
            List<FieldCondition> results = null;

            if (conditionFields != null && conditionFields.Count > 0)
            {
                results = new List<FieldCondition>();

                foreach (var cf in conditionFields)
                {
                    results.Add(new FieldCondition(cf, FieldConditionTypes.Equal));
                }
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields)
        {
            return GetSelectSQL(tableName, valueFields, conditionFields, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, bool distinct)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, null, distinct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="top"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, int top, bool distinct)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, top, null, distinct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetCountSQL(string tableName, List<string> conditionFields)
        {
            var temp = new List<FieldCondition>();

            if (conditionFields != null)
            {
                foreach (var field in conditionFields)
                {
                    temp.Add(new FieldCondition(field));
                }
            }

            return this.GetCountSQL(tableName, temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetCountSQL(string tableName, List<FieldCondition> conditionFields)
        {
            string conditionLists;

            if (conditionFields == null || conditionFields.Count == 0)
            {
                conditionLists = "";
            }
            else
            {
                var conditions = new List<string>();

                foreach (var condition in conditionFields)
                {
                    condition.DatabaseType = this.DatabaseType;
                    conditions.Add(condition.ToString());
                }

                conditionLists = "where " + string.Join(" and ", conditions.ToArray());
            }

            var sql = (DatabaseType == DatabaseTypes.SQL || DatabaseType == DatabaseTypes.SQL2)
                            ? string.Format("select count(*) as scount from {0} with (nolock) {1}", tableName, conditionLists)
                            : string.Format("select count(*) as scount from {0} {1}", tableName, conditionLists);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, List<OrderBy> orderBys)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, orderBys, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="orderBys"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, List<OrderBy> orderBys, bool distinct)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, orderBys, distinct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="top"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, int top, List<OrderBy> orderBys)
        {
            return GetSelectSQL(tableName, valueFields, conditionFields, top, orderBys, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="top"></param>
        /// <param name="orderBys"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, int top, List<OrderBy> orderBys, bool distinct)
        {
            var tops = GetTops(top);
            var valueLists = GetValueLists(valueFields);
            var dist = distinct ? "distinct " : "";

            string conditionLists;

            if (conditionFields == null || conditionFields.Count == 0)
            {
                conditionLists = "";
            }
            else
            {
                var conditions = new List<string>();

                foreach (var condition in conditionFields)
                {
                    condition.DatabaseType = this.DatabaseType;
                    conditions.Add(condition.ToString());
                }

                conditionLists = "where " + string.Join(" and ", conditions.ToArray());
            }

            var orderby = this.GetOrderBy(orderBys);
            var sql = (DatabaseType == DatabaseTypes.SQL || DatabaseType == DatabaseTypes.SQL2)
                            ? string.Format("select {0}{1}{2} from {3} with (nolock) {4} {5}", dist, tops, valueLists, tableName, conditionLists, orderby)
                            : string.Format("select {0}{1}{2} from {3} {4} {5}", dist, tops, valueLists, tableName, conditionLists, orderby);

            return sql;
        }

        private string GetValueLists(List<string> valueFields)
        {
            string valueLists;

            if (valueFields == null || valueFields.Count == 0)
            {
                valueLists = "*";
            }
            else
            {
                var fixedFields = this.FixFieldName(valueFields);
                valueLists = string.Join(",", fixedFields.ToArray());
            }
            return valueLists;
        }

        private List<string> FixFieldName(List<string> valueFields)
        {
            var fields = new List<string>();

            foreach (var field in valueFields)
            {
                fields.Add(this.FixFieldName(field));
            }

            return fields;
        }

        private string FixFieldName(string name)
        {
            return this.DatabaseType == DatabaseTypes.SQL ? string.Format("[{0}]", name) : name;
        }

        private string GetTops(int top)
        {
            var tops = "";

            if (top > 0)
            {
                tops = string.Format("top {0} ", top);
            }
            return tops;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields, int top)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, top, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, int top)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, top, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <param name="orderBys"></param>
        /// <returns></returns>
        public string GetSelectSQL(string tableName, List<string> valueFields, List<string> conditionFields, List<OrderBy> orderBys)
        {
            return this.GetSelectSQL(tableName, valueFields, conditionFields, 0, orderBys);
        }

        private string GetOrderBy(List<OrderBy> orderBys)
        {
            var orderby = "";

            if (orderBys != null && orderBys.Count > 0)
            {
                var orderbyLists = new List<string>();

                foreach (var order in orderBys)
                {
                    orderbyLists.Add(string.Format("{0} {1}", this.FixFieldName(order.FieldName), this.GetSortOrder(order.OrderByType)));
                }

                orderby = "order by " + string.Join(", ", orderbyLists.ToArray());
            }

            return orderby;
        }

        private string GetSortOrder(OrderByTypes orderby)
        {
            string sortOrder;

            switch (orderby)
            {
                case OrderByTypes.Ascending:
                    sortOrder = "asc";
                    break;
                case OrderByTypes.Descending:
                    sortOrder = "desc";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("orderby");
            }

            return sortOrder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public string GetInsertSQL(string tableName, List<string> fields)
        {
            var fixedFields = this.FixFieldName(fields);
            var valueFields = string.Join(",", fixedFields.ToArray());
            var argFields = new List<string>();

            foreach (var field in fields)
            {
                argFields.Add(_parameterPreFix + field);
            }

            var value2 = string.Join(",", argFields.ToArray());
            var sql = string.Format("insert into {0}({1}) values({2})", tableName, valueFields, value2);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetUpdateSQL(string tableName, List<string> valueFields, List<FieldCondition> conditionFields)
        {
            if (valueFields == null || valueFields.Count == 0)
            {
                throw new ArgumentException("Could not be null or empty.", "valueFields");
            }

            var fieldLists = new List<string>();

            foreach (var field in valueFields)
            {
                fieldLists.Add(string.Format("{0}={1}{2}", this.FixFieldName(field), _parameterPreFix, field));
            }

            var values = string.Join(",", fieldLists.ToArray());
            string conditionLists;

            if (conditionFields == null || conditionFields.Count == 0)
            {
                conditionLists = "";
            }
            else
            {
                var conditions = new List<string>();

                foreach (var condition in conditionFields)
                {
                    condition.DatabaseType = this.DatabaseType;
                    conditions.Add(condition.ToString());
                }

                conditionLists = "where " + string.Join(" and ", conditions.ToArray());
            }

            var sql = string.Format("update {0} set {1} {2}", tableName, values, conditionLists);

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="valueFields"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetUpdateSQL(string tableName, List<string> valueFields, List<string> conditionFields)
        {
            var cfs = ConvertFieldCondition(conditionFields);

            return GetUpdateSQL(tableName, valueFields, cfs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="conditionFields"></param>
        /// <returns></returns>
        public string GetDeleteSQL(string tableName, List<string> conditionFields)
        {
            var conditionLists = new List<string>();

            if (conditionFields != null)
            {
                foreach (var field in conditionFields)
                {
                    conditionLists.Add(string.Format("{0}={1}{2}", this.FixFieldName(field), _parameterPreFix, field));
                }
            }

            var conditions = string.Join(" and ", conditionLists.ToArray());
            var where = conditionLists.Count > 0 ? "where" : "";
            var sql = string.Format("delete {0} {1} {2}", tableName, where, conditions);

            return sql;
        }
    }
}

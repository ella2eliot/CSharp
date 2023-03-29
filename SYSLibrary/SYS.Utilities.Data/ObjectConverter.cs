using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class ObjectConverter
    {
        /// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="row"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static bool Convert<T>(DataRow row, T item) where T : class, new()
        {
            var propertyChanged = false;
            var type = item.GetType();
            var properties = type.GetProperties();

            foreach (DataColumn column in row.Table.Columns)
            {
                var value = row[column.ColumnName] == DBNull.Value ? null : row[column.ColumnName];

                foreach (var property in properties)
                {
                    var attributes = (System.ComponentModel.DataAnnotations.Schema.ColumnAttribute[])property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), false);

                    if (attributes.Length > 0)
                    {
                        try
                        {
                            var attribute = attributes[0];

                            if (attribute.Name == column.ColumnName)
                            {
                                if (property.PropertyType == typeof(string))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new[] { value });
                                }
                                else if (property.PropertyType == typeof(int))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToInt32(value) });
                                }
                                else if (property.PropertyType == typeof(int?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToInt32(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(DateTime))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDateTime(value) });
                                }
                                else if (property.PropertyType == typeof(DateTime?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDateTime(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(double))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDouble(value) });
                                }
                                else if (property.PropertyType == typeof(double?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDouble(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(decimal))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDecimal(value) });
                                }
                                else if (property.PropertyType == typeof(decimal?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDecimal(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(Single))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToSingle(value) });
                                }
                                else if (property.PropertyType == typeof(Single?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToSingle(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(bool))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { value != null && (value.ToString() == "1" || value.ToString().ToUpper() == "TRUE") });
                                }
                                else if (property.PropertyType == typeof(bool?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { (value.ToString() == "1" || value.ToString().ToUpper() == "TRUE") });
                                    }
                                }
                                else if (property.PropertyType == typeof(byte))
                                {
                                    propertyChanged = true;
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToByte(value) });
                                }
                                else if (property.PropertyType == typeof(byte?))
                                {
                                    if (value != null)
                                    {
                                        propertyChanged = true;
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToByte(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(Guid))
                                {
                                    object guid = value == null ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(value.ToString());
                                    property.GetSetMethod().Invoke(item, new[] { guid });
                                }
                                else
                                {
                                    throw new ApplicationException("Unsupport property type for " + property.PropertyType);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Could not convert value for column '" + column.ColumnName + "'", ex);
                        }
                    }
                }
            }

            return propertyChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T Convert<T>(DataRow row) where T : class, new()
        {
            T item = new T();
            var type = item.GetType();
            var properties = type.GetProperties();

            foreach (DataColumn column in row.Table.Columns)
            {
                var value = row[column.ColumnName] == DBNull.Value ? null : row[column.ColumnName];

                foreach (var property in properties)
                {
                    var attributes = (System.ComponentModel.DataAnnotations.Schema.ColumnAttribute[])property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), false);

                    if (attributes.Length > 0)
                    {
                        try
                        {
                            var attribute = attributes[0];

                            if (attribute.Name == column.ColumnName)
                            {
                                if (property.PropertyType == typeof(string))
                                {
                                    property.GetSetMethod().Invoke(item, new[] { value });
                                }
                                else if (property.PropertyType == typeof(int))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToInt32(value) });
                                }
                                else if (property.PropertyType == typeof(int?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToInt32(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(DateTime))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDateTime(value) });
                                }
                                else if (property.PropertyType == typeof(DateTime?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDateTime(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(double))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDouble(value) });
                                }
                                else if (property.PropertyType == typeof(double?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDouble(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(decimal))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDecimal(value) });
                                }
                                else if (property.PropertyType == typeof(decimal?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToDecimal(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(Single))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToSingle(value) });
                                }
                                else if (property.PropertyType == typeof(Single?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToSingle(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(bool))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { value != null && (value.ToString() == "1" || value.ToString().ToUpper() == "TRUE") });
                                }
                                else if (property.PropertyType == typeof(bool?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { (value.ToString() == "1" || value.ToString().ToUpper() == "TRUE") });
                                    }
                                }
                                else if (property.PropertyType == typeof(byte))
                                {
                                    property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToByte(value) });
                                }
                                else if (property.PropertyType == typeof(byte?))
                                {
                                    if (value != null)
                                    {
                                        property.GetSetMethod().Invoke(item, new object[] { System.Convert.ToByte(value) });
                                    }
                                }
                                else if (property.PropertyType == typeof(Guid))
                                {
                                    object guid = value == null ? new Guid("00000000-0000-0000-0000-000000000000") : new Guid(value.ToString());
                                    property.GetSetMethod().Invoke(item, new[] { guid });
                                }
                                else
                                {
                                    throw new ApplicationException("Unsupport property type for " + property.PropertyType);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Could not convert value for column '" + column.ColumnName + "'", ex);
                        }
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> Convert<T>(DataTable table) where T : class, new()
        {
            List<T> results = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                results.Add(Convert<T>(row));
            }

            return results;
        }
    }

}

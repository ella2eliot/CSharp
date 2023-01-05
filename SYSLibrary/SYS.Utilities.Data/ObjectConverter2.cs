using SYS.Utilities.Data.TypeConverters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// List<T> Convert<T> with Parallel foreach
	/// </summary>
	public class ObjectConverter2
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Convert<T>(DataRow row, T item) where T : class, new()
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
                                var typeConverter = TypeConverterFactory.GetConverter(property.PropertyType);

                                propertyChanged = true;
                                property.GetSetMethod().Invoke(item, new[] { typeConverter.Convert(value) });
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
        public T Convert<T>(DataRow row) where T : class, new()
        {
            var item = new T();
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
                                var typeConverter = TypeConverterFactory.GetConverter(property.PropertyType);
                                property.GetSetMethod().Invoke(item, new[] { typeConverter.Convert(value) });
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
        public List<T> Convert<T>(DataTable table) where T : class, new()
        {
            var results = new List<T>();

            Parallel.ForEach(table.Rows.Cast<DataRow>(), row => results.Add(Convert<T>(row)));

            return results;
        }
    }
}

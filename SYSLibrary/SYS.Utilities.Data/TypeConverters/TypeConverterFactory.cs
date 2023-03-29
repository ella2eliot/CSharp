using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data.TypeConverters
{
    /// <summary>
	/// 
	/// </summary>
	internal class TypeConverterFactory
    {
        private static readonly Dictionary<Type, ITypeConverter> _converters = new Dictionary<Type, ITypeConverter>();
        private static readonly object _lockObject = new object();

        internal static ITypeConverter GetConverter(Type type)
        {
            Type converterType = null;

            if (type == typeof(string))
            {
                converterType = typeof(StringConverter);
            }
            else if (type == typeof(int))
            {
                converterType = typeof(IntegerConverter);
            }
            else if (type == typeof(int?))
            {
                converterType = typeof(NullableIntegerConverter);
            }
            else if (type == typeof(double))
            {
                converterType = typeof(DoubleConverter);
            }
            else if (type == typeof(double?))
            {
                converterType = typeof(NullableDoubleConverter);
            }
            else if (type == typeof(Single))
            {
                converterType = typeof(SingleConverter);
            }
            else if (type == typeof(Single?))
            {
                converterType = typeof(NullableSingleConverter);
            }
            else if (type == typeof(decimal))
            {
                converterType = typeof(DecimalConverter);
            }
            else if (type == typeof(decimal?))
            {
                converterType = typeof(NullableDecimalConverter);
            }
            else if (type == typeof(bool))
            {
                converterType = typeof(BooleanConverter);
            }
            else if (type == typeof(bool?))
            {
                converterType = typeof(NullableBooleanConverter);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                converterType = typeof(DateTimeConverter);
            }

            if (converterType == null)
            {
                throw new ApplicationException("Unsupport property type for " + type);
            }

            return GetConverter(type, converterType);
        }

        private static ITypeConverter GetConverter(Type type, Type converterType)
        {
            if (!_converters.ContainsKey(type))
            {
                lock (_lockObject)
                {
                    if (!_converters.ContainsKey(type))
                    {
                        var converter = (ITypeConverter)Activator.CreateInstance(converterType);
                        _converters.Add(type, converter);
                    }
                }
            }

            return _converters[type];
        }
    }
}

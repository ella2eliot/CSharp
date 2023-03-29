using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data.TypeConverters
{
    internal class NullableSingleConverter : ITypeConverter
    {
        public object Convert(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            return System.Convert.ToSingle(value);
        }
    }
}

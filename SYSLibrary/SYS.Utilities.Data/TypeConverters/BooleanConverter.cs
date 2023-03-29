using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data.TypeConverters
{
    internal class BooleanConverter : ITypeConverter
    {
        public object Convert(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }

            var s = value.ToString().ToUpper();

            return s == "1" || s == "TRUE";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities
{
    /// <summary>
	/// </summary>
	public class DateTimeTool
    {
        /// <summary>
        /// </summary>
        /// <param name = "date"></param>
        /// <returns></returns>
        public static DateTime Convert(string date)
        {
            date = date.Trim();

            DateTime result;
            if (date.Length == 8)
            {
                result = DateTime.Parse(string.Format("{0}/{1}/{2}", date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2)));
            }
            else if (date.Length == 14)
            {
                result = DateTime.Parse(string.Format("{0}/{1}/{2} {3}:{4}:{5}",
                                                      date.Substring(0, 4), date.Substring(4, 2), date.Substring(6, 2),
                                                      date.Substring(8, 2), date.Substring(10, 2), date.Substring(12, 2)));
            }
            else
            {
                result = DateTime.Parse(date);
            }

            return result;
        }
    }
}

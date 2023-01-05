using SYS.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Common
{
    /// <summary>
	/// 
	/// </summary>
	public class Guard
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        public static void ValidateParameter(object param)
        {
            if (param == null)
            {
                throw new NullParameterException();
            }
        }
    }
}

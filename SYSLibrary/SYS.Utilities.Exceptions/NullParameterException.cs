using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Exceptions
{
    /// <summary>
	/// 
	/// </summary>
	[Serializable]
    public class NullParameterException : ApplicationException
    {
        /// <summary>
		/// 
		/// </summary>
		public NullParameterException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public NullParameterException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NullParameterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected NullParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Exceptions
{
    /// <summary>
	/// </summary>
	[Serializable]
    public class DatabaseAccessFailedException : BaseException
    {
        /// <summary>
        /// </summary>
        public DatabaseAccessFailedException()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name = "message"></param>
        public DatabaseAccessFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public DatabaseAccessFailedException(string errorCode, string message)
            : base(errorCode, message)
        {

        }

        /// <summary>
        /// </summary>
        /// <param name = "message"></param>
        /// <param name = "innerException"></param>
        public DatabaseAccessFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name = "message"></param>
        /// <param name = "innerException"></param>
        public DatabaseAccessFailedException(string errorCode, string message, Exception innerException)
            : base(errorCode, message, innerException)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name = "info"></param>
        /// <param name = "context"></param>
        protected DatabaseAccessFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name = "info"></param>
        /// <param name = "context"></param>
        protected DatabaseAccessFailedException(string errorCode, SerializationInfo info, StreamingContext context)
            : base(errorCode, info, context)
        {
        }
    }
}

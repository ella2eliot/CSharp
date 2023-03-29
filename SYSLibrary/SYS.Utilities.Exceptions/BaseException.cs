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
    public abstract class BaseException : ApplicationException
    {
        /// <summary>
        /// unknow
        /// </summary>
        public const string Unknow = "unknow";

        /// <summary>
        /// 
        /// </summary>
        public string ErrorCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected BaseException()
        {
            this.ErrorCode = Unknow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        protected BaseException(string message)
            : this(Unknow, message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        protected BaseException(string errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected BaseException(string message, Exception innerException)
            : this(Unknow, message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        protected BaseException(string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BaseException(SerializationInfo info, StreamingContext context)
            : this(Unknow, info, context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected BaseException(string errorCode, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.ErrorCode = errorCode;
        }
    }
}

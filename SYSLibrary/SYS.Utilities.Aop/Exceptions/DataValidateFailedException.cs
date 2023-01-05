using System;
using System.Runtime.Serialization;

namespace SYS.Utilities.Aop.Exceptions
{
    ///<summary>
    ///</summary>
    [Serializable]
    public class DataValidateFailedException : Exception
    {
        ///<summary>
        ///</summary>
        public DataValidateFailedException()
        {
        }

        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        public DataValidateFailedException(string message)
              : base(message)
        {
        }

        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        ///<param name="innerException"></param>
        public DataValidateFailedException(string message, Exception innerException)
              : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DataValidateFailedException(SerializationInfo info, StreamingContext context)
              : base(info, context)
        {
        }
    }
}

using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionScopeHelper
    {
        /// <summary>
        /// Create the TransactionScope object with timeout 60 seconds and ReadCommitted isloation level.
        /// </summary>
        /// <returns></returns>
        public static TransactionScope CreateTransactionScope()
        {
            return CreateTransactionScope(60);
        }

        /// <summary>
        /// Create the TransactionScope object with specific isloation level. the timeout default is 60 seconds.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public static TransactionScope CreateTransactionScope(IsolationLevel isolationLevel)
        {
            return CreateTransactionScope(60, isolationLevel);
        }

        /// <summary>
        /// Create the TransactionScope object with specific timeout. the default isolation level is ReadCommitted
        /// </summary>
        /// <param name="timeout">Timeout in seconds</param>
        /// <returns></returns>
        public static TransactionScope CreateTransactionScope(int timeout)
        {
            return CreateTransactionScope(timeout, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Create the TransactionScope object with specific timeout and isloation level.
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public static TransactionScope CreateTransactionScope(int timeout, IsolationLevel isolationLevel)
        {
            var scopeOption = TransactionScopeOption.Required;
            var t = new TimeSpan(0, 0, 0, timeout);
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = t,
            };

            return new TransactionScope(scopeOption, transactionOptions);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class ConnectionConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public class MSSQL : Oracle
        {

            /// <summary>
            /// 
            /// </summary>
            public const string InitialCatalog = "Initial Catalog";
        }

        /// <summary>
        /// 
        /// </summary>
        public class Oracle
        {
            /// <summary>
            /// 
            /// </summary>
            public const string DataSource = "Data Source";

            /// <summary>
            /// 
            /// </summary>
            public const string UserID = "User ID";

            /// <summary>
            /// 
            /// </summary>
            public const string Password = "Password";

            /// <summary>
            /// 
            /// </summary>
            public const string ApplicationName = "Application Name";
        }
    }
}

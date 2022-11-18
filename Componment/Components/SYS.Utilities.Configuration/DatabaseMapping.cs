using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Configuration
{
    /// <summary>
	/// 
	/// </summary>
	public class DatabaseMapping
    {
        /// <summary>
        /// 預計連結之資料庫.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 實際連結資料庫.
        /// </summary>
        public string AppConfigName { get; set; }

        /// <summary>
        /// 資料庫類型.
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// Deafult constructor.
        /// </summary>
        public DatabaseMapping()
        {
            DbType = "DEFAULT";
        }
    }
}

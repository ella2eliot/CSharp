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
	public class AppSettingConfig
    {
        private string _name;

        private string _value;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}

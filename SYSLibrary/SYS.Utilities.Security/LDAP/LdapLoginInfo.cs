using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Security.LDAP
{
    /// <summary>
	/// 
	/// </summary>
	public class LdapLoginInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        public LdapLoginInfo(string domain, string username, string pwd)
        {
            Domain = domain;
            UserName = username;
            Password = pwd;
        }

        /// <summary>
        /// LDAP login user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// LDAP login password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Login user's domain.
        /// </summary>
        public string Domain { get; set; }
    }
}

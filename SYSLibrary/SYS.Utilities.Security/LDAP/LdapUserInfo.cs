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
	public class LdapUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lo"></param>
        public LdapUserInfo(LdapObject lo)
        {
            UserName = lo.Name;
            Department = lo.Department;
            SAMAccountName = lo.SAMAccountName;
            TelephoneNumber = lo.TelephoneNumber;
            Title = lo.Title;
            Location = lo.Location;
            Email = lo.EMailAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LdapUserInfo()
        {
        }

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Manager name.
        /// </summary>
        public LdapUserInfo Manager { get; set; }

        /// <summary>
        /// The primary telephone number.
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Contains the name for the department in which the user works.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SAMAccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// AD object location.
        /// </summary>
        public string DistinguishedName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectCategory { get; set; }
    }
}

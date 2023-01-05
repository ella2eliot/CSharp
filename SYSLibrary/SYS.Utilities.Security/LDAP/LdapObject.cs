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
	public class LdapObject
    {
        internal Dictionary<string, string> SchemaProperties;

        /// <summary>
        /// 
        /// </summary>
        public LdapObject()
        {
            ObjectClassType = ObjectClassType.None;
        }

        //		public LdapObject(string name) : this()
        //		{
        //			Name = name;
        //		}

        /// <summary>
        /// 
        /// </summary>
        public List<LdapObject> Members { get; set; }

        internal List<string> ActiveDirectoryMemberCollection { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Name
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Name); }
        }

        /// <summary>
        /// Manager name.
        /// </summary>
        public string Manager
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Manager); }
        }

        /// <summary>
        /// The primary telephone number.
        /// </summary>
        public string TelephoneNumber
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.TelephoneNumber); }
        }

        /// <summary>
        /// The user's company name.
        /// </summary>
        public string Company
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Company); }
        }

        /// <summary>
        /// Contains the name for the department in which the user works.
        /// </summary>
        public string Department
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Department); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string EMailAddress
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.EMailAddress); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Division
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Division); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CountryName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.CountryName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.DisplayName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Location
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Location); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizationName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.OrganizationName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizationalUnitName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.OrganizationalUnitName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PersonalTitle
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.PersonalTitle); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SAMAccountName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.SAMAccountName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.Title); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectClass
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.ObjectClass); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClassDisplayName
        {
            get { return GetAttributeValue(ActiveDirectoryAttributes.ClassDisplayName); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObjectClassType ObjectClassType { get; set; }

        private string GetAttributeValue(string attributeName)
        {
            return SchemaProperties.ContainsKey(attributeName) ? SchemaProperties[attributeName] : "";
        }
    }
}

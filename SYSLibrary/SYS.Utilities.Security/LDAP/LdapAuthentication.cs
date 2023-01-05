using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Security.LDAP
{
    /// <summary>
	/// Active Directory(AD) login authentication service.
	/// </summary>
	public class LdapAuthentication
    {
        private const string LdapProtocol = "ldap://";
        private string _ldapPath;
        private string _filterAttribute;
        private readonly string _ldapServer;
        private Dictionary<string, string> _schemaProperties;
        private DirectorySearcher _searcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path"></param>
        public LdapAuthentication(string path)
        {
            _ldapPath = path;

            if (!path.ToLower().StartsWith(LdapProtocol.ToLower()))
            {
                _ldapPath = LdapProtocol + path;
            }

            _ldapServer = _ldapPath.ToLower().Replace(LdapProtocol, "");
        }


        /// <summary>
        /// Validate AD user's account and password.
        /// </summary>
        /// <param name="loginInfo">AD user information(domain, account name, password).</param>
        /// <returns>true, is validated, otherwise is not validated.</returns>
        public bool IsAuthenticated(LdapLoginInfo loginInfo)
        {
            return IsAuthenticated(loginInfo.Domain, loginInfo.UserName, loginInfo.Password);
        }

        /// <summary>
        /// Validate AD user.
        /// </summary>
        /// <param name="domain">Domain.</param>
        /// <param name="username">AD user account name.</param>
        /// <param name="pwd">AD user's password.</param>
        /// <returns>true, is validated, otherwise is not validated.</returns>        
        // 此程式碼使用 LDAP目錄提供者。驗證成功傳回 true。
        // Default_01_Login.aspx頁面中的程式碼會呼叫 .LdapAuthentication.IsAuthenticated()方法，並傳入從使用者收集的認證。
        // 接著，會使用目錄服務的路徑、使用者名稱與密碼建立 DirectoryEntry物件。
        // 使用者名稱的格式必須是 domain\username。
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username; // domain\username
            
            try
            {
                DirectoryEntry entry = new DirectoryEntry(_ldapPath, username, pwd);
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;
                //DirectoryEntry 物件接著會取得 NativeObject屬性，以嘗試強制 AdsObject 進行繫結。
                //若此動作成功，會取得使用者的 CN屬性，方式是建立 DirectorySearcher物件並篩選 sAMAccountName。

                _searcher = new DirectorySearcher(entry);

                _searcher.Filter = "(SAMAccountName=" + username + ")";
                _searcher.PropertiesToLoad.Add("cn");
                SearchResult result = _searcher.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _ldapPath = result.Path;
                _filterAttribute = (string)result.Properties["cn"][0];

                return true;
            }
            catch (Exception ex)
            {
                throw new ActiveDirectoryOperationException(ex.Message.Trim(), ex);                
            }
            
        }

        //取得使用者所屬的安全性與通訊群組清單，方式是建立 DirectorySearcher物件並根據 memberOf屬性進行篩選。
        //此方法會傳回由管道 (|) 分隔的群組清單。
        //每個群組的格式看起來像這樣 CN=...,...,DC=domain,DC=com
        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_ldapPath);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");   //傳回由管道 (|) 分隔的群組清單。
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }

        private void InitializeSearcher(string path, LdapLoginInfo loginInfo)
        {
            //			var domainAndUsername = loginInfo.Domain + @"\" + loginInfo.UserName;
            var entry = new DirectoryEntry(path, loginInfo.UserName, loginInfo.Password);
            //		    DirectoryEntry entry = new DirectoryEntry(path, domainAndUsername, loginInfo.Password);
            // 繫結至原始 AdsObject 以強制進行驗證。
            try
            {
                //				Object obj = entry.NativeObject;
            }
            catch (Exception ex)
            {
                //當出現"無法指出的錯誤"時, 可能是ldap 的路徑指定有誤.
                throw new ActiveDirectoryOperationException("Validate failed of errror : " + ex.Message.Trim(), ex);
            }

            _searcher = new DirectorySearcher(entry);
            _searcher.Sort = new SortOption("name", SortDirection.Ascending);
            _schemaProperties = GetActiveDirectorySchema(loginInfo);

            if (_schemaProperties != null)
            {
                foreach (var property in _schemaProperties)
                {
                    _searcher.PropertiesToLoad.Add(property.Key);
                }
            }
        }

        /// <summary>
        /// Search AD.
        /// </summary>
        /// <param name="filter"></param>
        private List<LdapObject> SearchActiveDirectory(string filter)
        {
            List<LdapObject> retval = null;

            _searcher.Filter = filter;

            using (var results = _searcher.FindAll())
            {
                if (results.Count > 0)
                {
                    retval = new List<LdapObject>();

                    foreach (SearchResult result in results)
                    {
                        LdapObject lo = new LdapObject { SchemaProperties = CopySchemaProperty() };

                        if (_schemaProperties != null)
                        {
                            foreach (var schemaProperty in _schemaProperties)
                            {
                                var propertyCollection = result.Properties[schemaProperty.Key];
                                var arr = new List<string>();

                                foreach (var p in propertyCollection)
                                {
                                    arr.Add(p.ToString());
                                }

                                lo.SchemaProperties[schemaProperty.Key] = propertyCollection.Count > 0 ? string.Join(", ", arr.ToArray()) : "";
                            }
                        }

                        if (result.Properties[ActiveDirectoryAttributes.ObjectClass].Contains("user"))
                        {
                            lo.ObjectClassType = ObjectClassType.User;
                        }
                        else if (result.Properties[ActiveDirectoryAttributes.ObjectClass].Contains("group"))
                        {
                            lo.ObjectClassType = ObjectClassType.Group;
                        }

                        lo.ActiveDirectoryMemberCollection = new List<string>();

                        foreach (string member in result.Properties[ActiveDirectoryAttributes.Member])
                        {
                            lo.ActiveDirectoryMemberCollection.Add(member);
                        }

                        retval.Add(lo);
                    }
                }
            }

            return retval;
        }

        private Dictionary<string, string> CopySchemaProperty()
        {
            var result = new Dictionary<string, string>();

            foreach (var property in _schemaProperties)
            {
                result.Add(property.Key, "");
            }

            return result;
        }

        private string GetObjectName(string member)
        {
            var temp = member.Split(new[] { ",OU" }, StringSplitOptions.RemoveEmptyEntries);
            temp = temp[0].Split(new[] { "CN=" }, StringSplitOptions.RemoveEmptyEntries);

            return temp[0].Replace("\\", "");
        }

        /// <summary>
        /// Get AD account information.
        /// </summary>
        /// <param name="loginInfo">Login AD account information.</param>
        /// <param name="searchUser">User to be get information.</param>
        /// <returns>Return the matched sear user account information, otherwise null.</returns>
        public LdapUserInfo GetUserInformation(LdapLoginInfo loginInfo, string searchUser)
        {
            InitializeSearcher(_ldapPath, loginInfo);

            var results = SearchActiveDirectory("(SAMAccountName=" + searchUser + ")");
            LdapUserInfo retval = null;

            if (results != null && results.Count > 0)
            {
                var result = results[0];

                retval = new LdapUserInfo();

                retval.UserName = result.Name;
                retval.TelephoneNumber = result.TelephoneNumber;
                retval.Department = result.Department;
                retval.Location = result.Location;
                retval.SAMAccountName = result.SAMAccountName;
                retval.Title = result.Title;
                retval.DistinguishedName = result.SchemaProperties[ActiveDirectoryAttributes.DistinguishedName];
                retval.ObjectCategory = result.SchemaProperties[ActiveDirectoryAttributes.ObjectCategory];
                retval.Email = result.SchemaProperties[ActiveDirectoryAttributes.EMailAddress];

                try
                {
                    var manager = GetObjectName(result.Manager);
                    var filter = "(name=" + manager + ")";

                    var managerResults = SearchActiveDirectory(filter);

                    if (managerResults != null && managerResults.Count > 0)
                    {
                        retval.Manager = new LdapUserInfo();

                        var managerResult = managerResults[0];
                        retval.Manager.UserName = managerResult.Name;
                    }
                }
                catch
                {
                    retval.Manager = null;
                }
            }

            return retval;
        }

        /// <summary>
        /// Get AD account information.
        /// </summary>
        /// <param name="loginInfo">Login AD account information.</param>
        /// <param name="searchMail">User to be get information.</param>
        /// <returns>Return the matched sear user account information, otherwise null.</returns>
        public LdapUserInfo GetMailInformation(LdapLoginInfo loginInfo, string searchMail)
        {
            InitializeSearcher(_ldapPath, loginInfo);

            var results = SearchActiveDirectory("(mail=" + searchMail + ")");
            LdapUserInfo retval = null;

            if (results != null && results.Count > 0)
            {
                var result = results[0];

                retval = new LdapUserInfo();

                retval.UserName = result.Name;
                retval.TelephoneNumber = result.TelephoneNumber;
                retval.Department = result.Department;
                retval.Location = result.Location;
                retval.SAMAccountName = result.SAMAccountName;
                retval.Title = result.Title;
                retval.DistinguishedName = result.SchemaProperties[ActiveDirectoryAttributes.DistinguishedName];
                retval.ObjectCategory = result.SchemaProperties[ActiveDirectoryAttributes.ObjectCategory];
                retval.Email = result.SchemaProperties[ActiveDirectoryAttributes.EMailAddress];

                try
                {
                    var manager = GetObjectName(result.Manager);
                    var filter = "(name=" + manager + ")";

                    var managerResults = SearchActiveDirectory(filter);

                    if (managerResults != null && managerResults.Count > 0)
                    {
                        retval.Manager = new LdapUserInfo();

                        var managerResult = managerResults[0];
                        retval.Manager.UserName = managerResult.Name;
                    }
                }
                catch
                {
                    retval.Manager = null;
                }
            }

            return retval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetActiveDirectorySchema(LdapLoginInfo loginInfo)
        {
            var result = new Dictionary<string, string>();
            var context = new DirectoryContext(DirectoryContextType.DirectoryServer, _ldapServer, loginInfo.UserName, loginInfo.Password);
            var schema = ActiveDirectorySchema.GetSchema(context);

            foreach (ActiveDirectorySchemaProperty schemaProperty in schema.FindAllProperties(PropertyTypes.Indexed | PropertyTypes.InGlobalCatalog))
            {
                result.Add(schemaProperty.Name, "");
            }

            string[] missKeys = new[]
                            {
                                ActiveDirectoryAttributes.TelephoneNumber,
                                ActiveDirectoryAttributes.Title,
                                ActiveDirectoryAttributes.Manager,
                                ActiveDirectoryAttributes.Member,
                                ActiveDirectoryAttributes.DistinguishedName,
                            };

            foreach (var missKey in missKeys)
            {
                if (!result.ContainsKey(missKey))
                {
                    result.Add(missKey, "");
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<LdapObject> GetActiveDirectoryObjects(LdapLoginInfo loginInfo, string groupName)
        {
            return GetActiveDirectoryObjects(loginInfo, groupName, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="groupName"></param>
        /// <param name="retrieveSubObjectDetail"></param>
        /// <returns></returns>
        public List<LdapObject> GetActiveDirectoryObjects(LdapLoginInfo loginInfo, string groupName, bool retrieveSubObjectDetail)
        {
            InitializeSearcher(_ldapPath, loginInfo);

            return GetActiveDirectoryObjects(groupName, retrieveSubObjectDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="retrieveSubObjectDetail"></param>
        /// <returns></returns>
        private List<LdapObject> GetActiveDirectoryObjects(string objectName, bool retrieveSubObjectDetail)
        {
            var filter = GetFilterString(objectName);

            var results = SearchActiveDirectory(filter);
            var retval = new List<LdapObject>();

            if (results != null && results.Count > 0)
            {
                foreach (var result in results)
                {
                    retval.Add(result);

                    if (result.ObjectClassType == ObjectClassType.Group && result.ActiveDirectoryMemberCollection != null)
                    {
                        result.Members = new List<LdapObject>();

                        if (retrieveSubObjectDetail)
                        {
                            var filterMemberStrings = new List<string>();

                            foreach (var member in result.ActiveDirectoryMemberCollection)
                            {
                                var memberName = GetObjectName(member);
                                filterMemberStrings.Add("(anr=" + memberName + ")");
                            }

                            var filterString = "(|" + string.Join("", filterMemberStrings.ToArray()) + ")";
                            var memberObjects = GetActiveDirectoryObjects(filterString, retrieveSubObjectDetail);

                            result.Members.AddRange(memberObjects);
                        }
                        else
                        {
                            foreach (var member in result.ActiveDirectoryMemberCollection)
                            {
                                var memberName = GetObjectName(member);
                                var item = new LdapObject
                                {
                                    SchemaProperties = new Dictionary<string, string>
                                                                          {
                                                                              {ActiveDirectoryAttributes.DisplayName, memberName},
                                                                              {ActiveDirectoryAttributes.Name, memberName}
                                                                          }
                                };

                                result.Members.Add(item);
                            }
                        }
                    }

                    if (result.Members != null && result.Members.Count > 0)
                    {
                        var sortedDictionary = new SortedDictionary<string, LdapObject>();

                        foreach (var member in result.Members)
                        {
                            sortedDictionary.Add(member.Name, member);
                        }

                        result.Members = new List<LdapObject>();
                        result.Members.AddRange(sortedDictionary.Values);
                    }
                }
            }


            return retval;
        }

        private string GetFilterString(string objectName)
        {
            if (objectName.Contains("(anr="))
            {
                return objectName;
            }

            return "(anr=" + objectName + ")";
        }
    }
}

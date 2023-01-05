using SYS.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    /// <summary>
	/// 
	/// </summary>
	public class DataHelper
    {
        private static readonly object _lockObject = new object();
        private static DataHelper _instance;
        private readonly DateTime _configLastLogTime = DateTime.MinValue;
        private int _sqlCommandTimeOut = 30;
        //default 10 minutes refresh config settings
        private int _configRefreshDuration = 600;

        private DataHelper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static DataHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataHelper();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ConfigRefreshDuration
        {
            get { return _configRefreshDuration; }
            set { _configRefreshDuration = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SQLCommandTimeOut
        {
            get
            {
                TimeSpan timeSpan = (DateTime.Now - _configLastLogTime);

                if (timeSpan.TotalSeconds > _configRefreshDuration || _configLastLogTime == DateTime.MinValue)
                {
                    lock (_lockObject)
                    {
                        NameValueCollection appSettings = ConfigManager.Instance.GetAppSettings();

                        string timeout = appSettings["SQLCommandTimeOut"];

                        if (!string.IsNullOrEmpty(timeout))
                        {
                            _sqlCommandTimeOut = int.Parse(timeout);
                        }
                    }
                }

                return _sqlCommandTimeOut;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public ApplicationAccountSetting ConvertToObject(string connectionString)
        {
            var strings = connectionString.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var dic = new Dictionary<string, string>();

            foreach (var s in strings)
            {
                var split = s.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                var key = "";
                var value = "";

                if (split.Length > 0)
                {
                    key = split[0].ToUpper(CultureInfo.InvariantCulture).Trim();
                }

                if (split.Length > 1)
                {
                    value = split[1].ToUpper(CultureInfo.InvariantCulture).Trim();
                }

                dic.Add(key, value);
            }

            var setting = new ApplicationAccountSetting();
            var dicKey = ConnectionConstants.Oracle.DataSource.ToUpper(CultureInfo.InvariantCulture);

            setting.DatabaseServer = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.MSSQL.InitialCatalog.ToUpper(CultureInfo.InvariantCulture);
            setting.DatabaseName = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.Oracle.UserID.ToUpper(CultureInfo.InvariantCulture);
            setting.Login = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.Oracle.Password.ToUpper(CultureInfo.InvariantCulture);
            setting.Password = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            dicKey = ConnectionConstants.MSSQL.ApplicationName.ToUpper(CultureInfo.InvariantCulture);
            setting.ApplicationName = dic.ContainsKey(dicKey) ? dic[dicKey] : "";

            return setting;
        }
    }
}

using SYS.Utilities.IO;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace SYS.Utilities.Configuration
{
    public class ConfigManager
    {
        private static volatile ConfigManager _instance;
        private static readonly object _lockObject = new object();

        /// <summary>
        /// 
        /// </summary>
        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConfigManager();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Save the application setting to the specific assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="appSetting"></param>
        public void Save(Assembly assembly, AppSettingConfig appSetting)
        {
            Save(appSetting, false, assembly);
        }

        /// <summary>
        /// Save the application setting to the calling assembly.
        /// </summary>
        /// <param name="appSetting"></param>
        public void Save(AppSettingConfig appSetting)
        {
            Save(appSetting, false, Assembly.GetCallingAssembly());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSetting"></param>
        /// <param name="createWhenNotExist"></param>
        /// <param name="assembly"></param>
        public void Save(AppSettingConfig appSetting, bool createWhenNotExist, Assembly assembly)
        {
            string codeBase = assembly.CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.AppSettings == null)
            {
                throw new ConfigurationErrorsException("No appSettings section in " + codeBase + ".config");
            }

            if (configuration.AppSettings.Settings[appSetting.Name] == null)
            {
                if (createWhenNotExist)
                {
                    configuration.AppSettings.Settings.Add(appSetting.Name, appSetting.Value);
                }
                else
                {
                    throw new ConfigurationErrorsException("No appSetting named '" + appSetting.Name + "'");
                }
            }
            else
            {
                configuration.AppSettings.Settings[appSetting.Name].Value = appSetting.Value;
            }

            configuration.Save();
        }

        /// <summary>
        /// Get connection string settings from calling assembly.
        /// </summary>
        /// <returns></returns>
        public ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return GetConnectionStrings(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConnectionStringSettingsCollection GetConnectionStringsFromExecuteAssembly()
        {
            return ConfigurationManager.ConnectionStrings;
        }

        /// <summary>
        /// Get connection string setting from specific name and assembly.
        /// </summary>
        /// <param name="connectionName">Connection string name.</param>
        /// <param name="assembly">Assembly that connection string stroed in.</param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName, Assembly assembly)
        {
            ConnectionStringSettingsCollection connectionStrings = GetConnectionStrings(assembly);
            ConnectionStringSettings connectionString = connectionStrings[connectionName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("Can not find the '" + connectionName + "' connectionstring setting.");
            }

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// Get connection string setting from specific name and assembly.
        /// </summary>
        /// <param name="connectionName">Connection string name.</param>
        /// <param name="assembly">Assembly that connection string stroed in.</param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName, Assembly assembly, DatabaseMapping mapping)
        {
            if (assembly == null)
            {
                return GetConnectionString(connectionName, mapping);
            }

            string realConnectionName = connectionName;

            if (mapping != null)
            {
                NameValueCollection appSettings = this.GetAppSettings(assembly);
                realConnectionName = appSettings[mapping.AppConfigName];

                if (string.IsNullOrEmpty(realConnectionName))
                {
                    throw new ConfigurationErrorsException("Can not find the '" + mapping.AppConfigName + "' appSettings.");
                }
            }

            ConnectionStringSettingsCollection connectionStrings = GetConnectionStrings(assembly);
            ConnectionStringSettings connectionString = connectionStrings[realConnectionName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("Can not find the '" + realConnectionName + "' connectionstring setting.");
            }

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// Get connection string settings from specific assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public ConnectionStringSettingsCollection GetConnectionStrings(Assembly assembly)
        {
            string codeBase = assembly.CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.ConnectionStrings == null)
            {
                throw new ConfigurationErrorsException("No ConnectionStrings section in " + codeBase + ".config");
            }

            return configuration.ConnectionStrings.ConnectionStrings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public NameValueCollection GetAppSettings(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            string codeBase = assembly.CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.AppSettings == null)
            {
                throw new ConfigurationErrorsException("No AppSettings section in " + codeBase + ".config");
            }

            NameValueCollection result = new NameValueCollection();

            foreach (string key in configuration.AppSettings.Settings.AllKeys)
            {
                result.Add(key, configuration.AppSettings.Settings[key].Value);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="assembly"></param>
        /// <returns>Return the value of specific key in appSettings.</returns>
        public string GetAppSetting(string key, Assembly assembly)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var appSettings = assembly == null ? this.GetAppSettings() : this.GetAppSettings(assembly);
            var value = appSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException("Could not find the key '" + key + "' in appSettings.");
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return the value of specific key in appSettings.</returns>
        public string GetAppSetting(string key)
        {
            return this.GetAppSetting(key, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public NameValueCollection GetAppSettings()
        {
            //			string codeBase = Assembly.GetCallingAssembly().CodeBase;
            //
            //			codeBase = PathTool.RemoveCodeBase(codeBase);
            //
            //			System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);
            //
            //			if (configuration.AppSettings == null)
            //			{
            //				throw new ConfigurationErrorsException("No AppSettings section in " + codeBase + ".config");
            //			}
            //
            //			NameValueCollection result = new NameValueCollection();
            //
            //			foreach (string key in configuration.AppSettings.Settings.AllKeys)
            //			{
            //				result.Add(key, configuration.AppSettings.Settings[key].Value);
            //			}
            //
            //			return result;
            ConfigurationManager.RefreshSection("appSettings");
            return ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteAppSettings()
        {
            string codeBase = Assembly.GetCallingAssembly().CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.AppSettings != null)
            {
                configuration.AppSettings.Settings.Clear();
            }

            configuration.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteConnectionSettings()
        {
            string codeBase = Assembly.GetCallingAssembly().CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.ConnectionStrings != null)
            {
                configuration.ConnectionStrings.ConnectionStrings.Clear();
            }

            configuration.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSetting"></param>
        /// <param name="createWhenNotExist"></param>
        public void Save(AppSettingConfig appSetting, bool createWhenNotExist)
        {
            Save(appSetting, createWhenNotExist, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void Save(ConnectionStringSettings settings)
        {
            if (ConfigurationManager.ConnectionStrings == null)
            {
                throw new ConfigurationErrorsException("No connectionStrings section in config file");
            }

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[settings.Name];

            if (connectionString == null)
            {
                ConfigurationManager.ConnectionStrings.Add(settings);
            }
            else
            {
                connectionString.ConnectionString = settings.ConnectionString;
                connectionString.ProviderName = settings.ProviderName;
            }

            //			ConfigurationManager.Save();

            //			Save(settings, false, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="createWhenNotExist"></param>
        /// <param name="assembly"></param>
        public void Save(ConnectionStringSettings settings, bool createWhenNotExist, Assembly assembly)
        {
            string codeBase = assembly.CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            if (configuration.ConnectionStrings == null)
            {
                throw new ConfigurationErrorsException("No connectionStrings section in " + codeBase + ".config");
            }

            ConnectionStringSettings connectionString = configuration.ConnectionStrings.ConnectionStrings[settings.Name];

            if (connectionString == null)
            {
                if (createWhenNotExist)
                {
                    configuration.ConnectionStrings.ConnectionStrings.Add(settings);
                }
                else
                {
                    throw new ConfigurationErrorsException("No connectionString named '" + settings.Name + "'");
                }
            }
            else
            {
                connectionString.ConnectionString = settings.ConnectionString;
                connectionString.ProviderName = settings.ProviderName;
            }

            configuration.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="createWhenNotExist"></param>
        public void Save(ConnectionStringSettings settings, bool createWhenNotExist)
        {
            Save(settings, createWhenNotExist, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName)
        {
            ConnectionStringSettingsCollection connectionStrings = GetConnectionStringsFromExecuteAssembly();
            ConnectionStringSettings connectionString = connectionStrings[connectionName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("Can not find the '" + connectionName + "' connectionstring setting.");
            }

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public string GetConnectionString(string connectionName, DatabaseMapping mapping)
        {
            string realConnectionName = connectionName;

            if (mapping != null)
            {
                NameValueCollection appSettings = this.GetAppSettings();
                realConnectionName = appSettings[mapping.AppConfigName];
            }

            ConnectionStringSettingsCollection connectionStrings = GetConnectionStringsFromExecuteAssembly();
            ConnectionStringSettings connectionString = connectionStrings[realConnectionName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("Can not find the '" + realConnectionName + "' connectionstring setting.");
            }

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// 取得自訂區段
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="sectionName">自訂區段名稱</param>
        /// <returns></returns>
        public NameValueCollection GetCustomSection(Assembly assembly, string sectionName)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            string codeBase = assembly.CodeBase;

            codeBase = PathTool.RemoveCodeBase(codeBase);

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(codeBase);

            AppSettingsSection customSection = (AppSettingsSection)configuration.GetSection(sectionName);

            if (customSection == null)
            {
                throw new ConfigurationErrorsException("No " + sectionName + " section in " + codeBase + ".config");
            }

            NameValueCollection result = new NameValueCollection();

            foreach (string key in customSection.Settings.AllKeys)
            {
                result.Add(key, customSection.Settings[key].Value);
            }

            return result;
        }
    }
}

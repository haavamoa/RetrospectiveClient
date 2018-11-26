using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Retrospective.Clients.WPF.Configuration.Interfaces;

namespace Retrospective.Clients.WPF.Configuration
{
    class AppSettings : IAppSettings
    {
        public NameValueCollection ReadAllSettings()
        {
            return ConfigurationManager.AppSettings;
        }

        public string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                return "Not Found";
            }
        }

        public bool TryUpdateAppSettings(string key, string value)
        {
            bool updated;
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                updated = true;
            }
            catch (ConfigurationErrorsException)
            {
                updated = false;
            }
            return updated;
        }

        public void AddEmptySettingIfMissing(params string[] keys)
        {
            foreach (var key in keys.Where(key => ReadSetting(key) == null))
            {
                TryUpdateAppSettings(key, "");
            }
        }
    }
}
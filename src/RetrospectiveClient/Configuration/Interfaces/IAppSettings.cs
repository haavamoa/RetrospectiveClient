using System.Collections.Specialized;

namespace RetrospectiveClient.Configuration.Interfaces
{
    public interface IAppSettings
    {
        NameValueCollection ReadAllSettings();
        string ReadSetting(string key);
        bool TryUpdateAppSettings(string key, string value);
        void AddEmptySettingIfMissing(params string[] keys);
    }
}
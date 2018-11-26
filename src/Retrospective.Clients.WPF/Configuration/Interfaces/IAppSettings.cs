using System.Collections.Specialized;

namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    public interface IAppSettings
    {
        NameValueCollection ReadAllSettings();
        string ReadSetting(string key);
        bool TryUpdateAppSettings(string key, string value);
        void AddEmptySettingIfMissing(params string[] keys);
    }
}
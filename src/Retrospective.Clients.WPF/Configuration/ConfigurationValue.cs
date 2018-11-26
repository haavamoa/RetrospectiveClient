using GalaSoft.MvvmLight;
using Retrospective.Clients.WPF.Configuration.Interfaces;
using Retrospective.Clients.WPF.Configuration.Slack;

namespace Retrospective.Clients.WPF.Configuration
{
    public class ConfigurationValue : ObservableObject, IConfigurationValue
    {
        private readonly IAppSettings m_appSettings;
        private readonly IEvaluateConfiguration m_configurationEvaluater;
        private string m_value;

        public ConfigurationValue(
            IAppSettings appSettings,
            IEvaluateConfiguration configurationEvaluater,
            string key,
            string displayName,
            bool isRequired = false,
            string defaultValue = "")
        {
            m_appSettings = appSettings;
            m_configurationEvaluater = configurationEvaluater;
            IsRequired = isRequired;
            Key = key;
            DisplayName = displayName;
            Value = appSettings.ReadSetting(key);
            if (string.IsNullOrEmpty(Value))
            {
                if (string.IsNullOrEmpty(defaultValue))
                {
                    appSettings.AddEmptySettingIfMissing(key);
                }
                else
                {
                    appSettings.TryUpdateAppSettings(Key, defaultValue);
                    Value = defaultValue;
                }
            }
        }

        public string Value
        {
            get => m_value;
            set
            {
                m_appSettings.TryUpdateAppSettings(Key, value);
                Set(ref m_value, value);
                RaisePropertyChanged(() => IsRequiredAndMissingValue);
                m_configurationEvaluater.Evaluate();
            }
        }

        public bool IsRequiredAndMissingValue => IsRequired && string.IsNullOrEmpty(Value);
        public bool IsRequired { get; }
        public string Key { get; }
        public string DisplayName { get; }
    }
}
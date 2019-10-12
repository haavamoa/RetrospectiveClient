using GalaSoft.MvvmLight;
using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.Configuration.Slack;
using RetrospectiveClient.Role_Interfaces;
using RetrospectiveClient.ViewModel;

namespace RetrospectiveClient.Configuration
{
    class UserConfiguration : ObservableObject, IUserConfiguration, IEvaluateConfiguration
    {
        
        private readonly IAppSettings m_appSettings;

        public UserConfiguration(IAppSettings appSettings, ISlackConfiguration slackConfiguration, ITeamConfiguration teamConfiguration, IZoomConfiguration zoomConfiguration)
        {
            m_appSettings = appSettings;
            SlackConfiguration = slackConfiguration;
            TeamConfiguration = teamConfiguration;
            ZoomConfiguration = zoomConfiguration;
        }

        public ISlackConfiguration SlackConfiguration { get; }
        public ITeamConfiguration TeamConfiguration { get; }
        public bool IsAllRequiredSet {
            get {
                return SlackConfiguration.IsAllRequiredSet && TeamConfiguration.IsAllRequiredSet && ZoomConfiguration.IsAllRequiredSet;
            }
        }

        public IZoomConfiguration ZoomConfiguration { get; set; }

        public void Initialize(ILog logger)
        {
            logger.Log<Info>("Reading all settings from appsettings");
            ReadAndMapAllSettings();
        }

        private void ReadAndMapAllSettings()
        {
            SlackConfiguration.Initialize(m_appSettings, this);
            TeamConfiguration.Initialize(m_appSettings, this);
            ZoomConfiguration.Initialize(m_appSettings, this);
        }

        public void Evaluate()
        {
             RaisePropertyChanged(() => IsAllRequiredSet);   
        }
    }
}
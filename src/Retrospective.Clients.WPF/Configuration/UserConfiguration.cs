using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Retrospective.Clients.WPF.Configuration.Interfaces;
using Retrospective.Clients.WPF.Configuration.Slack;
using Retrospective.Clients.WPF.Configuration.Team;
using Retrospective.Clients.WPF.Role_Interfaces;
using Retrospective.Clients.WPF.ViewModel;

namespace Retrospective.Clients.WPF.Configuration
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
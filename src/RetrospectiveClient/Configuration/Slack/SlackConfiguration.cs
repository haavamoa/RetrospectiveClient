using RetrospectiveClient.Configuration.Interfaces;

namespace RetrospectiveClient.Configuration.Slack
{
    class SlackConfiguration : ISlackConfiguration
    {

        public bool IsAllRequiredSet => WebHook!= null && !WebHook.IsRequiredAndMissingValue;
        public IConfigurationValue AnnouncementMessage { get; set; }

        public void Initialize(IAppSettings appSettings, IEvaluateConfiguration configurationEvaluater)
        {
            WebHook = new ConfigurationValue(appSettings, configurationEvaluater, "SlackWebHook", "Web Hook", true);
            AnnouncementMessage = new ConfigurationValue(appSettings, configurationEvaluater, "SlackAnnouncementMessage", "Announcement message", defaultValue: "@here : Retrospective has started");
        }

        public IConfigurationValue WebHook { get; set; }
    }
}
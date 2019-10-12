using RetrospectiveClient.Configuration.Slack;

namespace RetrospectiveClient.Configuration.Interfaces
{
    public interface ISlackConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue WebHook { get; set; }
        IConfigurationValue AnnouncementMessage { get; set; }
    }

    public interface ITopLevelConfiguration
    {
        void Initialize(IAppSettings appSettings, IEvaluateConfiguration configurationEvaluater);
        bool IsAllRequiredSet { get; }
    }
}
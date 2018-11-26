using Retrospective.Clients.WPF.Configuration.Slack;
using Retrospective.Clients.WPF.Configuration.Team;

namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    public interface ISlackConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue WebHook { get; set; }
        bool IsAllRequiredSet { get; }
        IConfigurationValue AnnouncementMessage { get; set; }
    }

    public interface ITopLevelConfiguration
    {
        void Initialize(IAppSettings appSettings, IEvaluateConfiguration configurationEvaluater);
        bool IsAllRequiredSet { get; }
    }
}
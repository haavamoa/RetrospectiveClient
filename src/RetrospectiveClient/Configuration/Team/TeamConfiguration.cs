using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.Configuration.Slack;

namespace RetrospectiveClient.Configuration.Team
{
    public class TeamConfiguration : ITeamConfiguration
    {

        public IConfigurationValue Name { get; set; }

        public void Initialize(IAppSettings appSettings, IEvaluateConfiguration configurationEvaluator)
        {
            Name = new ConfigurationValue(appSettings, configurationEvaluator, "TeamName", "Name");
        }

        public bool IsAllRequiredSet => true;
    }
}
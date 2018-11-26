using System;
using Retrospective.Clients.WPF.Configuration.Interfaces;
using Retrospective.Clients.WPF.Configuration.Slack;

namespace Retrospective.Clients.WPF.Configuration.Team
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
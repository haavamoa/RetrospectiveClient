using Retrospective.Clients.WPF.Configuration.Team;

namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    public interface ITeamConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue Name { get; set; }
    }
}
using Retrospective.Clients.WPF.Role_Interfaces;

namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    public interface IUserConfiguration
    {
        ISlackConfiguration SlackConfiguration { get; }
        ITeamConfiguration TeamConfiguration { get; }
        bool IsAllRequiredSet { get; }
        IZoomConfiguration ZoomConfiguration { get; set; }
        void Initialize(ILog logger);
    }
}
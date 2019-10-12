using RetrospectiveClient.Role_Interfaces;

namespace RetrospectiveClient.Configuration.Interfaces
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
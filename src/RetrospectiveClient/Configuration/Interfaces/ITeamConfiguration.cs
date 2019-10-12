namespace RetrospectiveClient.Configuration.Interfaces
{
    public interface ITeamConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue Name { get; set; }
    }
}
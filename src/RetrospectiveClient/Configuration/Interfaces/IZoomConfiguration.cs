namespace RetrospectiveClient.Configuration.Interfaces
{
    public interface IZoomConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue ZoomLevel { get; set; }
    }
}
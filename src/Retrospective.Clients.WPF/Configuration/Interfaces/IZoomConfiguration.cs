namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    public interface IZoomConfiguration : ITopLevelConfiguration
    {
        IConfigurationValue ZoomLevel { get; set; }
    }
}
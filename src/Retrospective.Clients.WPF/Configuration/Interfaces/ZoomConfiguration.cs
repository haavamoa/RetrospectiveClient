using Retrospective.Clients.WPF.Configuration.Slack;

namespace Retrospective.Clients.WPF.Configuration.Interfaces
{
    class ZoomConfiguration : IZoomConfiguration
    {
        public IConfigurationValue ZoomLevel { get; set; }
        public void Initialize(IAppSettings appSettings, IEvaluateConfiguration configurationEvaluater)
        {
            ZoomLevel = new ConfigurationValue(appSettings, configurationEvaluater, "ZoomLevel", "ZoomLevel", defaultValue: "1");
        }

        public bool IsAllRequiredSet => true;
    }
}
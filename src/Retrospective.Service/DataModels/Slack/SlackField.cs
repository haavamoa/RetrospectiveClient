using Newtonsoft.Json;

namespace Retrospective.Service.DataModels.Slack
{
    public class SlackField
    {
        public string Value { get; set; }
        public string Title { get; set; }
        [JsonProperty("short")]
        public bool IsShort { get; set; }
    }
}
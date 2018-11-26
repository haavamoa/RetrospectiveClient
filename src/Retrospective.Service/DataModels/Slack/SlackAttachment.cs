using System.Collections.Generic;
using Newtonsoft.Json;

namespace Retrospective.Service.DataModels.Slack
{
    public class SlackAttachment
    {
        public string Color { get; set; }
        public string Title { get; set; }
        public List<SlackField> Fields { get; set; }
        public string Footer { get; set; }
        [JsonProperty("ts")]
        public string TimeStamp { get; set; }
    }
}
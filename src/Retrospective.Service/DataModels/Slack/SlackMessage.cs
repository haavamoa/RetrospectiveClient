using System.Collections.Generic;
using Newtonsoft.Json;

namespace Retrospective.Service.DataModels.Slack
{
    public class SlackMessage
    {
        public string Text { get; set; }
        public List<SlackAttachment> Attachments { get; set; }   
        [JsonProperty("link_names")]
        public int CanLinkNames { get; set; }

    }
}
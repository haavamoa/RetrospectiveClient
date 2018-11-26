using Newtonsoft.Json;

namespace Retrospective.Service.DataModels
{
    public class Action
    {
        public string Text { get; set; }

        [JsonIgnore]
        public bool IsValid => !string.IsNullOrEmpty(Text);
    }
}
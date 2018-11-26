using System.Collections.Generic;
using Newtonsoft.Json;

namespace Retrospective.Service.DataModels
{
    public class Negative
    {
        public string Text { get; set; }
        public List<Action> Actions { get; set; }

        [JsonIgnore]
        public bool IsValid => !string.IsNullOrEmpty(Text);
    }
}
using Newtonsoft.Json;

namespace Retrospective.Service.DataModels
{
    public class Writer
    {
        public string Name { get; set; }
        public string NickName { get; set; }

        [JsonIgnore]
        public bool IsValid => !string.IsNullOrEmpty(Name);
    }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Retrospective.Service.DataModels.Slack;

namespace Retrospective.Service.Utils
{
    public static class SlackMessageExtensions
    {
            public static string Serialize(this SlackMessage slackMessage)
            {
                var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

                return JsonConvert.SerializeObject(slackMessage, serializerSettings);
            }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Retrospective.Service.DataModels;
using Retrospective.Service.DataModels.Slack;
using Retrospective.Service.Services.Slack.Interfaces;
using Retrospective.Service.Utils;

namespace Retrospective.Service.Services.Slack
{
    public class SlackService : ISlackService
    {
        private readonly ISlackMessageBuilder m_messageBuilder;
        private readonly HttpClient httpClient = new HttpClient();
        private const string NegativeColor = "#c3453c";
        private const string PositiveColor = "#89b14e";
        private const string NeutralColor = "#4674b9";

        public SlackService(ISlackMessageBuilder messageBuilder)
        {
            m_messageBuilder = messageBuilder;
        }

        public async Task<bool> TryAnnounceRetro(
            string webHook,
            string announcement = "@here : Retrospective has started",
            CancellationToken token = default(CancellationToken))
        {
            //var slackMessage = m_messageBuilder.AddText(announcement).Build();
            var slackMessage = m_messageBuilder.AddAttachement(
                                                               null,
                                                               "transient",
                                                               slackFieldsFunc: () => new List<SlackField>()
                                                               {
                                                                   new SlackField() { Title = "Retrospective Announcement", Value = announcement }
                                                               }).Build();

            var content = SerializeAndCreateStringContent(slackMessage);

            var response = await httpClient.PostAsync(webHook, content, token);

            return response.StatusCode == HttpStatusCode.OK;
        }

        private static StringContent SerializeAndCreateStringContent(SlackMessage slackMessage)
        {
            var slackMessageJson = slackMessage.Serialize();

            return new StringContent(slackMessageJson);
        }

        public async Task<bool> TryPostRetro(string webHook, Retro retro, CancellationToken token = default(CancellationToken))
        {
            var duration = retro.EndTime - retro.StartTime;
            var durationString = $"{duration.Hours}h:{duration.Minutes}m:{duration.Seconds}s";
            m_messageBuilder.AddAttachement(
                                            "Retrospective summary",
                                            color: "transient",
                                            footer: $"Writer: @{retro.Writer.NickName}\nDuration: {durationString}");

            if (retro.Positives != null && retro.Positives.Any())
            {
                m_messageBuilder.AddAttachement(
                                                "Positives",
                                                color: PositiveColor,
                                                slackFieldsFunc: () =>
                                                    retro.Positives.Select(positive => new SlackField { Value = "- "+positive.Text }).ToList());
            }

            if (retro.Negatives != null && retro.Negatives.Any())
            {
                m_messageBuilder.AddAttachement(
                                                "Negatives",
                                                color: NegativeColor,
                                                slackFieldsFunc: () =>
                                                    retro.Negatives.Select(negative => new SlackField { Value = "- " + negative.Text }).ToList());
            }

            if (retro.Actions != null && retro.Actions.Any())
            {
                m_messageBuilder.AddAttachement(
                                                "Actions",
                                                color: NeutralColor,
                                                slackFieldsFunc: () =>
                                                    retro.Actions.Select(action => new SlackField { Value = "- " + action.Text }).ToList());
            }

            var slackMessage = m_messageBuilder.Build();

            var content = SerializeAndCreateStringContent(slackMessage);

            var response = await httpClient.PostAsync(webHook, content, token);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
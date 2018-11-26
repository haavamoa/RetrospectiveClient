using System;
using System.Collections.Generic;
using Retrospective.Service.DataModels.Slack;

namespace Retrospective.Service.Services.Slack.Interfaces
{
    public interface ISlackMessageBuilder
    {
        SlackMessage Build();
        ISlackMessageBuilder AddText(string text);

        ISlackMessageBuilder AddAttachement(
            string title,
            string color = "good",
            Func<List<SlackField>> slackFieldsFunc = null,
            string footer = null,
            DateTime? timestamp = null);
    }
}
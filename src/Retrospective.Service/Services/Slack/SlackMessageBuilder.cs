using System;
using System.Collections.Generic;
using Retrospective.Service.DataModels.Slack;
using Retrospective.Service.Services.Slack.Interfaces;

namespace Retrospective.Service.Services.Slack
{
    public class SlackMessageBuilder : ISlackMessageBuilder
    {
        private SlackMessage m_slackMessage;

        public SlackMessageBuilder()
        {
            ResetValues();
        }

        public SlackMessage Build()
        {
            var slackMessageCopy = m_slackMessage;

            ResetValues();

            return slackMessageCopy;
        }

        public ISlackMessageBuilder AddText(string text)
        {
            var replacedText = ReplacePublicTagWords(text);

            m_slackMessage.Text = replacedText;
            return this;
        }

        public ISlackMessageBuilder AddAttachement(
            string title,
            string color = "good",
            Func<List<SlackField>> slackFieldsFunc = null,
            string footer = null,
            DateTime? timestamp = null)
        {
            if (m_slackMessage.Attachments == null)
            {
                m_slackMessage.Attachments = new List<SlackAttachment>();
            }

            var slackAttachment = new SlackAttachment { Title = title, Color = color };

            if (slackFieldsFunc != null)
            {
                slackAttachment.Fields = slackFieldsFunc();
            }

            slackAttachment.Footer = footer;

            if (timestamp != null)
            {
                var epocTimeStamp = (long)(timestamp.Value - new DateTime(1970, 1, 1)).TotalSeconds;
                slackAttachment.TimeStamp = epocTimeStamp.ToString();
            }

            m_slackMessage.Attachments.Add(slackAttachment);

            return this;
        }

        private void ResetValues()
        {
            m_slackMessage = new SlackMessage() { CanLinkNames = 1};
        }

        private static string ReplacePublicTagWords(string text)
        {
            var newtext = text.Replace("@here", "!here");
            newtext = text.Replace("@everyone", "!everyone");
            newtext = text.Replace("@channel", "!channel");
            return newtext;
        }
    }
}
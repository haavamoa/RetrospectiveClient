using System;
using FluentAssertions;
using Retrospective.Service.Services.Slack;
using Retrospective.Service.Tests.Utils;
using Xunit;

namespace Retrospective.Service.Tests.Services
{
    public class SlackServiceTests
    {
        public SlackServiceTests()
        {
            m_cut = new SlackService(new SlackMessageBuilder());
        }

        private SlackService m_cut;

        [Fact]
        public async void PostRetro()
        {
            var retro = new RetroBuilder().ValidRetro().Build();
            retro.Writer.NickName = "hmo";
            retro.StartTime = DateTime.Now;
            retro.EndTime = DateTime.Now;
            var posted = await m_cut.TryPostRetro("https://hooks.slack.com/services/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", retro);
            posted.Should().BeTrue();
        }

        [Fact]
        public async void PostAnnouncement()
        {
            var posted = await m_cut.TryAnnounceRetro("https://hooks.slack.com/services/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", "@here, @channel, @everyone to retro");
            posted.Should().BeTrue();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Retrospective.Service.DataModels;

namespace Retrospective.Service.Services.Slack.Interfaces
{
    public interface ISlackService
    {
        Task<bool> TryAnnounceRetro(string webHook, string announcement = "@here : Retrospective has started", CancellationToken token = default(CancellationToken));
        Task<bool> TryPostRetro(string webHook, Retro retro, CancellationToken token = default(CancellationToken));
    }
}
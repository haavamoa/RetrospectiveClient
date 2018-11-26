using System.Runtime.CompilerServices;
using Retrospective.Clients.WPF.ViewModel;

namespace Retrospective.Clients.WPF.Role_Interfaces
{
    public interface ILog
    {
        void Log<T>(string message,[CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0) where T : ILogLevel, new();
    }
}
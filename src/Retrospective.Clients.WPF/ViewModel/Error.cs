using Retrospective.Clients.WPF.Models;

namespace Retrospective.Clients.WPF.ViewModel
{
    public class Error : ILogLevel
    {
        public LogLevel Enum => LogLevel.Error;
    }

    public class Info : ILogLevel
    {
        public LogLevel Enum => LogLevel.Info;
    }

    public class Warning : ILogLevel
    {
        public LogLevel Enum => LogLevel.Warning;
    }
}
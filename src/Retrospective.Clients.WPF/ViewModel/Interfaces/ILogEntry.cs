using System;
using Retrospective.Clients.WPF.Models;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface ILogEntry
    {
        DateTime TimeStamp { get; }
        string Message { get; }
        LogLevel LogLevel { get; }
        string CallerName { get; }
        int CallerLineNumber { get; }
    }
}
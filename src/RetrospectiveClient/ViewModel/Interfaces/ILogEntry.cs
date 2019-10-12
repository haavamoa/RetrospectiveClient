using System;
using RetrospectiveClient.Models;

namespace RetrospectiveClient.ViewModel.Interfaces
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
using System;
using System.Runtime.CompilerServices;
using RetrospectiveClient.ViewModel;

namespace RetrospectiveClient.Role_Interfaces
{
    public interface ILog
    {
        void Log<T>(string message,[CallerMemberName] string callerName = "", [CallerLineNumber] int callerLineNumber = 0) where T : ILogLevel, new();
        void Log<T>(Exception e, string callerName = "", int callerLineNumber = 0) where T : ILogLevel, new();
    }
}
using System;
using Retrospective.Clients.WPF.ViewModel;
using Retrospective.Clients.WPF.ViewModel.Interfaces;

namespace Retrospective.Clients.WPF.Models
{
    public class LogEntry : ILogEntry
    {
        public LogEntry(string message, DateTime timeStamp, LogLevel logLevel, string callerName, int callerLineNumber)
        {
            Message = message;
            TimeStamp = timeStamp;
            LogLevel = logLevel;
            CallerName = callerName;
            CallerLineNumber = callerLineNumber;
        }

        public DateTime TimeStamp { get; }
        public string Message { get; }
        public LogLevel LogLevel { get; }

        public string CallerName { get; }

        public int CallerLineNumber { get; }
        }
    }
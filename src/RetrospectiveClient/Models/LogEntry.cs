using System;
using RetrospectiveClient.ViewModel.Interfaces;

namespace RetrospectiveClient.Models
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using RetrospectiveClient.Models;
using RetrospectiveClient.ViewModel.Interfaces;
using LogLevel = RetrospectiveClient.Models.LogLevel;

namespace RetrospectiveClient.ViewModel
{
    class LogViewModel : ViewModelBase, ILogViewModel
    {
        private readonly List<ILogEntry> m_logEntries;
        private object m_isOpen;
        private CollectionView m_observableLogEntries;

        public LogViewModel()
        {
            m_logEntries = new List<ILogEntry>();
            ObservableLogEntries = new CollectionView(m_logEntries);
            OpenLogCommand = new RelayCommand(() => IsOpen = true);
            CopyLogEntryCommand = new RelayCommand<ILogEntry>(CopyLogEntryToClipBoard);
        }

        public object IsOpen
        {
            get => m_isOpen;
            set => Set(ref m_isOpen, value);
        }

        public ICommand OpenLogCommand { get; }
        public int NumberOfLogEntries => m_logEntries.Count;
        public ICommand CopyLogEntryCommand { get; }

        public CollectionView ObservableLogEntries
        {
            get => m_observableLogEntries;
            set => Set(ref m_observableLogEntries, value);
        }

        private void CopyLogEntryToClipBoard(ILogEntry logEntry)
        {
            var copiedString = $"\n {nameof(logEntry.LogLevel)} : {logEntry.LogLevel} |" +
                               $"\n {nameof(logEntry.TimeStamp)} : {logEntry.TimeStamp.ToString()} |" +
                               $"\n {nameof(logEntry.Message)} : {logEntry.Message} |" + 
                               $"\n {nameof(logEntry.CallerName)} : {logEntry.CallerName} |" +
                               $"\n {nameof(logEntry.CallerLineNumber)} : {logEntry.CallerName}";
            Clipboard.SetText(copiedString);
        }

        public void Log<T>(Exception e, string callerName = "", int callerLineNumber = 0) where T : ILogLevel, new()
        {
            var logInstance = new T();
            if (logInstance.Enum == LogLevel.Error)
            {
                Crashes.TrackError(e);
            }

            Log<T>(e.Message, callerName, callerLineNumber);
        }

        public void Log<T>(string message, string callerName = "", int callerLineNumber = 0) where T : ILogLevel, new()
        {
            var logInstance = new T();

            if (logInstance.Enum == LogLevel.Error)
            {
                IsOpen = true;
            }

            m_logEntries.Add(new LogEntry(message, DateTime.Now, logInstance.Enum, callerName, callerLineNumber));
            var ordered = m_logEntries.OrderByDescending(l => l.TimeStamp);
            ObservableLogEntries = new CollectionView(ordered);
            RaisePropertyChanged(() => NumberOfLogEntries);
        }
    }
}
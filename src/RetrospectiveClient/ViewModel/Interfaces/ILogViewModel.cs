using System;
using System.Windows.Data;
using System.Windows.Input;
using RetrospectiveClient.Role_Interfaces;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface ILogViewModel : ILog
    {
        CollectionView ObservableLogEntries { get; }
        object IsOpen { get; set; }
        ICommand OpenLogCommand { get; }
        int NumberOfLogEntries { get; }
        ICommand CopyLogEntryCommand { get; }
    }
}
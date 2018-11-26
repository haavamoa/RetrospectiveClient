using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using Retrospective.Clients.WPF.Role_Interfaces;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
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
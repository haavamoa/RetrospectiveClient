using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IPositivesViewModel
    {
        ObservableCollection<Positive> Positives { get; set; }
        void Initialize(List<Positive> positives);
    }
}
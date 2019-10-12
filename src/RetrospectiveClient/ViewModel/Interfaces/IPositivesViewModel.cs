using System.Collections.Generic;
using System.Collections.ObjectModel;
using Retrospective.Service.DataModels;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface IPositivesViewModel
    {
        ObservableCollection<Positive> Positives { get; set; }
        void Initialize(List<Positive> positives);
    }
}
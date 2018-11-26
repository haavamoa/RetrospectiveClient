using System.Collections.Generic;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface INegativesViewModel
    {
        void Initialize(List<Negative> negatives);
    }
}
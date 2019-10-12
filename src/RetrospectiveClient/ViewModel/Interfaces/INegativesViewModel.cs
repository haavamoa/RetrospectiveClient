using System.Collections.Generic;
using Retrospective.Service.DataModels;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface INegativesViewModel
    {
        void Initialize(List<Negative> negatives);
    }
}
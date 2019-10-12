using System.Collections.Generic;
using Retrospective.Service.DataModels;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface IActionsViewModel
    {
        void Initialize(List<Action> actions);
    }
}
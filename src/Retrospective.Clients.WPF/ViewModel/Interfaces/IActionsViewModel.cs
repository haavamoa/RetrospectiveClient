using System.Collections.Generic;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IActionsViewModel
    {
        void Initialize(List<Action> actions);
    }
}
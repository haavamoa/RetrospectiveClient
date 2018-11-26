using Retrospective.Clients.WPF.Role_Interfaces;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IRetroViewModel : IHandleModelChanged
    {
        void Initialize(IHandleRetroChanged retroChangedHandler, ILog logger);
    }

}
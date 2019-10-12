using RetrospectiveClient.Role_Interfaces;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface IRetroViewModel : IHandleModelChanged
    {
        void Initialize(IHandleRetroChanged retroChangedHandler, ILog logger);
    }

}
using Retrospective.Service.DataModels;
using RetrospectiveClient.Role_Interfaces;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface IWriterViewModel
    {
        string Name { get; set; }
        string NickName { get; set; }
        void Initialize(Writer writer, IHandleModelChanged modelChangedHandler);
    }
}
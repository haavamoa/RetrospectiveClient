using Retrospective.Clients.WPF.Role_Interfaces;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IWriterViewModel
    {
        string Name { get; set; }
        string NickName { get; set; }
        void Initialize(Writer writer, IHandleModelChanged modelChangedHandler);
    }
}
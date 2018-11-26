using System.Windows.Input;
using Retrospective.Clients.WPF.Configuration.Interfaces;
using Retrospective.Clients.WPF.Role_Interfaces;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IConfigurationViewModel
    {
        void Initialize(ILog logger);
        IUserConfiguration UserConfiguration { get; }
        bool IsOpen { get; set; }
        ICommand OpenConfigurationCommand { get; }
    }
}
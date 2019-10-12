using System.Windows.Input;
using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.Role_Interfaces;

namespace RetrospectiveClient.ViewModel.Interfaces
{
    public interface IConfigurationViewModel
    {
        void Initialize(ILog logger);
        IUserConfiguration UserConfiguration { get; }
        bool IsOpen { get; set; }
        ICommand OpenConfigurationCommand { get; }
    }
}
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.Role_Interfaces;
using RetrospectiveClient.ViewModel.Interfaces;

namespace RetrospectiveClient.ViewModel
{
    public class ConfigurationViewModel : ViewModelBase, IConfigurationViewModel
    {
        private bool m_isOpen;

        public ConfigurationViewModel(IUserConfiguration userConfiguration)
        {
            UserConfiguration = userConfiguration;
            OpenConfigurationCommand = new RelayCommand(() => IsOpen = true);
        }

        public IUserConfiguration UserConfiguration { get; private set; }

        public bool IsOpen
        {
            get => m_isOpen;
            set => Set(ref m_isOpen, value);
        }

        public ICommand OpenConfigurationCommand { get; private set; }

        public void Initialize(ILog logger)
        {
            UserConfiguration.Initialize(logger);
        }
    }
}
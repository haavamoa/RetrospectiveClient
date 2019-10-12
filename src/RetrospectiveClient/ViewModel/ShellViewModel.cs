using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls.Dialogs;
using Retrospective.Service.Services.Interfaces;
using RetrospectiveClient.ViewModel.Interfaces;

namespace RetrospectiveClient.ViewModel
{
    public class ShellViewModel : ViewModelBase, IHandleRetroChanged
    {
        private readonly IRetroService m_retroService;
        private readonly IDialogCoordinator m_dialogCoordinator;

        public ShellViewModel(
            IZoomViewModel zoomViewModel,
            IRetroViewModel retroViewModel,
            IConfigurationViewModel configurationViewModel,
            ILogViewModel logViewModel,
            IRetroService retroService,
            IDialogCoordinator dialogCoordinator)
        {
            m_retroService = retroService;
            m_dialogCoordinator = dialogCoordinator;
            RetroViewModel = retroViewModel;
            UserConfigurationViewModel = configurationViewModel;
            ZoomViewModel = zoomViewModel;
            LogViewModel = logViewModel;
        }

        public ILogViewModel LogViewModel { get; set; }

        public IRetroViewModel RetroViewModel { get; }
        public IConfigurationViewModel UserConfigurationViewModel { get; private set; }

        public IZoomViewModel ZoomViewModel { get; }

        public void OnRetroCancelled()
        {
            RetroViewModel.Initialize(this, LogViewModel);
            RaisePropertyChanged(string.Empty);
        }

        public void OnRetroFinished()
        {
            RetroViewModel.Initialize(this, LogViewModel);
            RaisePropertyChanged(string.Empty);
        }

        public async Task Initialize()
        {
            var progressDialog = await m_dialogCoordinator.ShowProgressAsync(this, "Initializing Retrospecive Client", "Initializing Retrospecive Client...");
            progressDialog.SetProgress(0);
            try
            {
                progressDialog.SetMessage("Initializing User Configuration");
                UserConfigurationViewModel.Initialize(LogViewModel);
                progressDialog.SetProgress(0.33);

                RaisePropertyChanged(() => UserConfigurationViewModel);
                RaisePropertyChanged(() => ZoomViewModel);

                progressDialog.SetMessage("Checking User Configuration");
                if (!UserConfigurationViewModel.UserConfiguration.IsAllRequiredSet)
                {
                    UserConfigurationViewModel.OpenConfigurationCommand.Execute(null);
                }
                progressDialog.SetProgress(0.66);

                progressDialog.SetMessage("Preparing retrospective");
                RetroViewModel.Initialize(this, LogViewModel);
                RaisePropertyChanged(() => RetroViewModel);
                progressDialog.SetProgress(1);
            }
            catch (Exception e)
            {
                LogViewModel.Log<Error>(e);
            }

            await progressDialog.CloseAsync();
        }
    }

    public interface IHandleRetroChanged
    {
        void OnRetroCancelled();
        void OnRetroFinished();
    }
}
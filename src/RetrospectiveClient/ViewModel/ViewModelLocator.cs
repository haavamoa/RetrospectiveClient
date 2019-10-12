/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Retrospective.Clients.WPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls.Dialogs;
using Retrospective.Service.Repositories;
using Retrospective.Service.Services;
using Retrospective.Service.Services.Interfaces;
using Retrospective.Service.Services.Slack;
using Retrospective.Service.Services.Slack.Interfaces;
using RetrospectiveClient.Configuration;
using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.Configuration.Slack;
using RetrospectiveClient.Configuration.Team;
using RetrospectiveClient.ViewModel;
using RetrospectiveClient.ViewModel.Interfaces;

namespace Retrospective.Clients.WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            try
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

                ////if (ViewModelBase.IsInDesignModeStatic)
                ////{
                ////    // Create design time view services and models
                ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
                ////}
                ////else
                ////{
                ////    // Create run time view services and models
                ////    SimpleIoc.Default.Register<IDataService, DataService>();
                ////}
                SimpleIoc.Default.Register(CreateUserConfiguration);
                SimpleIoc.Default.Register(CreateJsonRepository);
                SimpleIoc.Default.Register(CreateRetroService);
                SimpleIoc.Default.Register(CreateSlackRetroService);
                SimpleIoc.Default.Register<IDialogCoordinator, DialogCoordinator>();
                SimpleIoc.Default.Register<IZoomViewModel, ZoomViewModel>();
                SimpleIoc.Default.Register<IRetroViewModel, RetroViewModel>();
                SimpleIoc.Default.Register<IWriterViewModel, WriterViewModel>();
                SimpleIoc.Default.Register<IConfigurationViewModel, ConfigurationViewModel>();
                SimpleIoc.Default.Register<ILogViewModel, LogViewModel>();
                SimpleIoc.Default.Register<ShellViewModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private ISlackService CreateSlackRetroService()
        {
            return new SlackService(new SlackMessageBuilder());
        }

        private IUserConfiguration CreateUserConfiguration()
        {
            return new UserConfiguration(new AppSettings(), new SlackConfiguration(), new TeamConfiguration(), new ZoomConfiguration());
        }

        private IRetroService CreateRetroService()
        {
            return new RetroService(SimpleIoc.Default.GetInstance<IRetroRepository>());
        }

        private IRetroRepository CreateJsonRepository()
        {
            //Can configure this
            return new JsonRepository();
        }

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
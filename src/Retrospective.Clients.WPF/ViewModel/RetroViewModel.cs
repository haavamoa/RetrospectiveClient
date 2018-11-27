using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;
using Retrospective.Clients.WPF.Configuration.Interfaces;
using Retrospective.Clients.WPF.Role_Interfaces;
using Retrospective.Clients.WPF.Utils;
using Retrospective.Clients.WPF.ViewModel.Interfaces;
using Retrospective.Service.DataModels;
using Retrospective.Service.Services.Interfaces;
using Retrospective.Service.Services.Slack.Interfaces;
using Action = Retrospective.Service.DataModels.Action;

namespace Retrospective.Clients.WPF.ViewModel
{
    public class RetroViewModel : ViewModelBase, IRetroViewModel
    {
        private readonly IDialogCoordinator m_dialogCoordinator;
        private readonly IRetroService m_retroService;
        private readonly ISlackService m_slackService;
        private readonly IUserConfiguration m_userConfiguration;
        private NotesViewModel<Action> m_actionsViewModel;
        private CancellationTokenSource m_cancellationTokenSource;
        private bool m_isStarted;
        private ILog m_logger;
        private NotesViewModel<Negative> m_negativesViewModel;
        private NotesViewModel<Positive> m_positivesViewModel;
        private Retro m_retro;
        private IHandleRetroChanged m_retroChangedHandler;
        private Writer m_writer;

        public RetroViewModel(
            IWriterViewModel writerViewModel,
            IDialogCoordinator dialogCoordinator,
            ISlackService slackService,
            IUserConfiguration userConfiguration,
            IRetroService retroService)
        {
            m_dialogCoordinator = dialogCoordinator;
            WriterViewModel = writerViewModel;
            StartCommand = new AsyncCommand(async _ => await StartRetro(), _ => !string.IsNullOrEmpty(m_writer?.NickName));
            SetPositivesFocusCommand = new RelayCommand(() => SetNotesFocus(shouldPositivesBeFocused: true));
            SetNegativesFocusCommand = new RelayCommand(() => SetNotesFocus(shouldNegativesBeFocused: true));
            SetActionsFocusCommand = new RelayCommand(() => SetNotesFocus(shouldActionsBeFocused: true));
            CreateFreshNotes();
            m_slackService = slackService;
            m_userConfiguration = userConfiguration;
            m_retroService = retroService;
            m_cancellationTokenSource = new CancellationTokenSource();
            FinishCommand = new AsyncCommand(async _ => await FinishRetro());
            CancelCommand = new RelayCommand(CancelRetro);
        }

        public AsyncCommand FinishCommand { get; }
        public ICommand CancelCommand { get; }

        public IWriterViewModel WriterViewModel { get; }

        public bool IsStarted
        {
            get => m_isStarted;
            set => Set(ref m_isStarted, value);
        }

        public AsyncCommand StartCommand { get; }

        public NotesViewModel<Positive> PositivesViewModel
        {
            get => m_positivesViewModel;
            private set => Set(ref m_positivesViewModel, value);
        }

        public NotesViewModel<Negative> NegativesViewModel
        {
            get => m_negativesViewModel;
            private set => Set(ref m_negativesViewModel, value);
        }

        public NotesViewModel<Action> ActionsViewModel
        {
            get => m_actionsViewModel;
            private set => Set(ref m_actionsViewModel, value);
        }

        public ICommand SetActionsFocusCommand { get; }
        public ICommand SetPositivesFocusCommand { get; }
        public ICommand SetNegativesFocusCommand { get; }

        public void Initialize(IHandleRetroChanged retroChangedHandler, ILog logger)
        {
            m_retroChangedHandler = retroChangedHandler;
            m_logger = logger;
            m_writer = new Writer();
            WriterViewModel.Initialize(m_writer, this);
            RaisePropertyChanged(() => WriterViewModel);
        }

        public void ModelChanged()
        {
            StartCommand.RaiseCanExecuteChanged();
            FinishCommand.RaiseCanExecuteChanged();
        }

        private void CancelRetro()
        {
            m_logger.Log<Info>("Cancelling retro");
            CreateFreshNotes();
            m_retro = null;
            IsStarted = false;
            m_retroChangedHandler.OnRetroCancelled();
            ModelChanged();
            m_logger.Log<Info>("Retro cancelled");
        }

        private void CreateFreshNotes()
        {
            PositivesViewModel = new NotesViewModel<Positive>();
            NegativesViewModel = new NotesViewModel<Negative>();
            ActionsViewModel = new NotesViewModel<Action>();
        }

        private async Task FinishRetro()
        {
            m_logger.Log<Info>("Finishing retro");
            var savingProgressDialog = await m_dialogCoordinator.ShowProgressAsync(this, "Saving retrospective", "Saving retrospective...");
            try
            {
                m_retro.Positives = new List<Positive>(PositivesViewModel.Notes.Where(n => !string.IsNullOrEmpty(n.Text)));
                m_retro.Negatives = new List<Negative>(NegativesViewModel.Notes.Where(n => !string.IsNullOrEmpty(n.Text)));
                m_retro.Actions = new List<Action>(ActionsViewModel.Notes.Where(n => !string.IsNullOrEmpty(n.Text)));
                m_retro.EndTime = DateTime.Now;

                if (m_retro.IsValid)
                {
                    var saved = await m_retroService.TrySaveAsync(m_retro);
                    savingProgressDialog.SetProgress(0.5);

                    if (saved)
                    {
                        m_logger.Log<Info>("Retro was saved");
                        savingProgressDialog.SetMessage("Posting retrospective to slack");

                        var posted = await m_slackService.TryPostRetro(m_userConfiguration.SlackConfiguration.WebHook.Value, m_retro);
                        savingProgressDialog.SetProgress(1);
                        if (posted)
                        {
                            m_logger.Log<Info>("Retro was posted to Slack");
                            await m_dialogCoordinator.ShowMessageAsync(
                                                                       this,
                                                                       "Successfully posted retrospective to Slack",
                                                                       "Thank you for using Retrospective Client");
                            CreateFreshNotes();
                            m_retro = null;
                            IsStarted = false;
                            m_retroChangedHandler.OnRetroFinished();
                            ModelChanged();
                        }
                        else
                        {
                            m_logger.Log<Error>("Something went wrong while posting retrospective to slack, please contact support.");
                        }
                    }
                    else
                    {
                        m_logger.Log<Error>("Something went wrong while saving retrospective, please contact support.");
                    }
                }
            }
            catch (Exception e)
            {
                await ShowGenericErrorMessage();
                m_logger.Log<Error>(e.Message);
            }
            finally
            {
                await savingProgressDialog.CloseAsync();
            }
        }

        private void OnCancelProgress(object sender, EventArgs e)
        {
            m_cancellationTokenSource.Cancel();
            m_cancellationTokenSource.Dispose();
        }

        private void SetNotesFocus(bool shouldPositivesBeFocused = false, bool shouldNegativesBeFocused = false, bool shouldActionsBeFocused = false)
        {
            if (shouldPositivesBeFocused)
            {
                PositivesViewModel.IsFocused = true;
                NegativesViewModel.IsFocused = false;
                ActionsViewModel.IsFocused = false;
            }
            else if (shouldNegativesBeFocused)
            {
                PositivesViewModel.IsFocused = false;
                NegativesViewModel.IsFocused = true;
                ActionsViewModel.IsFocused = false;
            }
            else if (shouldActionsBeFocused)
            {
                PositivesViewModel.IsFocused = false;
                NegativesViewModel.IsFocused = false;
                ActionsViewModel.IsFocused = true;
            }
        }

        private async Task ShowGenericErrorMessage()
        {
            await m_dialogCoordinator.ShowMessageAsync(this, "Something went wrong", "Check log and contact support.");
        }

        private async Task StartRetro()
        {
            m_logger.Log<Info>("A new retro is started");
            try
            {
                if (m_cancellationTokenSource.IsCancellationRequested)
                {
                    m_cancellationTokenSource = new CancellationTokenSource();
                }
                m_retro = new Retro(Guid.NewGuid()) { StartTime = DateTime.Now, Writer = m_writer };

                var dialogMessageResult = await m_dialogCoordinator.ShowMessageAsync(
                                                                                     this,
                                                                                     "Start retrospective",
                                                                                     "Getting ready to start the retrospective. Do you want to post a announcement on slack?",
                                                                                     MessageDialogStyle.AffirmativeAndNegative,
                                                                                     new MetroDialogSettings
                                                                                     {
                                                                                         AffirmativeButtonText = "Yes",
                                                                                         NegativeButtonText = "No",
                                                                                         DefaultButtonFocus = MessageDialogResult.Affirmative
                                                                                     });
                if (dialogMessageResult == MessageDialogResult.Affirmative)
                {
                    ProgressDialogController progressDialogController = null;
                    try
                    {
                        progressDialogController = await m_dialogCoordinator.ShowProgressAsync(
                                                                                               this,
                                                                                               "Announcing on slack",
                                                                                               $"Announcing \"{m_userConfiguration.SlackConfiguration.AnnouncementMessage.Value}\"",
                                                                                               true);
                        progressDialogController.Canceled += OnCancelProgress;
                        var announced = await m_slackService.TryAnnounceRetro(
                                                                              m_userConfiguration.SlackConfiguration.WebHook.Value,
                                                                              m_userConfiguration.SlackConfiguration.AnnouncementMessage.Value,
                                                                              m_cancellationTokenSource.Token);
                        if (announced)
                        {
                            await progressDialogController.CloseAsync();
                            m_logger.Log<Info>("Retro was announced to Slack");
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is TaskCanceledException)
                        {
                            if (progressDialogController != null)
                            {
                                progressDialogController.Canceled -= OnCancelProgress;
                                m_logger.Log<Info>("Cancelled posting announcement to slack");
                            }
                        }
                        else
                        {
                            if (progressDialogController != null)
                            {
                                if (progressDialogController.IsOpen)
                                {
                                    await progressDialogController.CloseAsync();
                                }
                            }
                        }

                        await ShowGenericErrorMessage();
                        throw;
                    }
                }

                PositivesViewModel.Initialize(new List<Positive>());
                NegativesViewModel.Initialize(new List<Negative>());
                ActionsViewModel.Initialize(new List<Action>());
                RaisePropertyChanged(() => PositivesViewModel);
                RaisePropertyChanged(() => NegativesViewModel);
                RaisePropertyChanged(() => ActionsViewModel);
                IsStarted = true;
                m_logger.Log<Info>("Retro was succesfully started");
                ModelChanged();

            }
            catch (Exception e)
            {
                m_logger.Log<Error>(e.Message);
            }
        }
    }
}
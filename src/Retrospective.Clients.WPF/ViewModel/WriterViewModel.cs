using GalaSoft.MvvmLight;
using Retrospective.Clients.WPF.Role_Interfaces;
using Retrospective.Clients.WPF.ViewModel.Interfaces;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel
{
    class WriterViewModel : ViewModelBase, IWriterViewModel
    {
        private Writer m_writer;
        private IHandleModelChanged m_modelChangedHandler;

        public string Name
        {
            get => m_writer?.Name;
            set
            {
                m_writer.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        public string NickName
        {
            get => m_writer?.NickName;
            set
            {
                m_writer.NickName = value;
                RaisePropertyChanged(() => NickName);
                m_modelChangedHandler?.ModelChanged();
            }
        }
        public void Initialize(Writer writer, IHandleModelChanged modelChangedHandler)
        {
            m_modelChangedHandler = modelChangedHandler;
            m_writer = writer;
            RaisePropertyChanged(() => NickName);
        }
    }
}
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RetrospectiveClient.Configuration.Interfaces;
using RetrospectiveClient.ViewModel.Interfaces;

namespace RetrospectiveClient.ViewModel
{
    public class ZoomViewModel : ViewModelBase, IZoomViewModel
    {
        private readonly IUserConfiguration m_userConfiguration;
        private const double ZoomFactor = 0.2;

        public ZoomViewModel(IUserConfiguration userConfiguration)
        {
            m_userConfiguration = userConfiguration;
            ZoomInCommand = new RelayCommand(() => { Zoomlevel += ZoomFactor; });
            ZoomOutCommand = new RelayCommand(() => { Zoomlevel -= ZoomFactor; });
        }

        public double ZoomLevelPercentage => Zoomlevel * 100;

        public double Zoomlevel
        {
            get
            {
                if (m_userConfiguration.ZoomConfiguration.ZoomLevel != null)
                {
                    return double.Parse(m_userConfiguration.ZoomConfiguration.ZoomLevel?.Value);
                }

                return 0;
            }
            private set
            {
                m_userConfiguration.ZoomConfiguration.ZoomLevel.Value = value.ToString();
                RaisePropertyChanged(() => Zoomlevel);
                RaisePropertyChanged(nameof(ZoomLevelPercentage));
            }
        }

        public ICommand ZoomInCommand { get; }
        public ICommand ZoomOutCommand { get; }
    }
}
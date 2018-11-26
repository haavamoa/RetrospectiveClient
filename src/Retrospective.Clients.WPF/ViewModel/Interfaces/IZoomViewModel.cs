using System.Windows.Input;

namespace Retrospective.Clients.WPF.ViewModel.Interfaces
{
    public interface IZoomViewModel
    {
        double Zoomlevel { get; }
        double ZoomLevelPercentage { get; }
        ICommand ZoomInCommand { get; }
        ICommand ZoomOutCommand { get; }
    }
}
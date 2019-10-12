using System.Windows;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace RetrospectiveClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppCenter.Start("06bc8f7c-4be1-41f7-94f1-9660d715a940",
                typeof(Analytics), typeof(Crashes));
            AppCenter.Start("06bc8f7c-4be1-41f7-94f1-9660d715a940",
                typeof(Analytics), typeof(Crashes));
        }
    }
}

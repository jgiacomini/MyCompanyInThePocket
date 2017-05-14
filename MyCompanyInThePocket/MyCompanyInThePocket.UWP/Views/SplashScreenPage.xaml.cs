using MyCompanyInThePocket.Core.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyCompanyInThePocket.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreenPage : Page
    {
        public SplashScreenPage()
        {
            this.InitializeComponent();
            DataContext = new SplashScreenViewModel();
            this.Loaded += SplashScreenView_Loaded;
        }

        private void SplashScreenView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            OnInitialize();
        }

        public Task OnInitialize()
        {
            var vm = DataContext as SplashScreenViewModel;
            return vm?.InitializeAsync();
        }
    }
}

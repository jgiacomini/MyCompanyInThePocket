using MvvmCross.Core.ViewModels;
using MvvmCross.WindowsUWP.Views;
using MyCompanyInThePocket.Core.ViewModels;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreenView : MvxWindowsPage
    {
        public SplashScreenView()
        {
            this.InitializeComponent();
            this.Loaded += SplashScreenView_Loaded;
        }

        private void SplashScreenView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            OnInitialize();
        }

        public Task OnInitialize()
        {
            var vm = (SplashScreenViewModel)this.ViewModel;
            return vm.InitializeAsync();
        }
    }
}

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.WindowsUWP.Platform;
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.UWP.Helpers;
using Windows.UI.Xaml.Controls;

namespace MyCompanyInThePocket.UWP
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeLastChance()
        {
            Mvx.RegisterType<ISqliteConnectionFactory, UWPSqliteConnectionFactory>();
            Mvx.RegisterType<IAuthentificationPlatformFactory, AuthentificationPlatformFactory>();

            base.InitializeLastChance();
        }
    }
}

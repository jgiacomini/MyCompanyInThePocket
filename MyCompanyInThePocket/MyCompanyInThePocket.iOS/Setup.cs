using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MyCompanyInThePocket.Core.Helpers;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }
        
        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

		protected override void InitializeLastChance()
		{
			Mvx.RegisterType<ISqliteConnectionFactory, IOSSqliteConnectionFactory>();
			Mvx.RegisterType<IAuthentificationPlatformFactory, AuthentificationPlatformFactory>();
			base.InitializeLastChance();
		}
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}

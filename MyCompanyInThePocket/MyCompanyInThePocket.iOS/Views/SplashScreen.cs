using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.iOS.Views
{
	public partial class SplashScreenView : BaseScreen<SplashScreenViewModel>
    {
        public SplashScreenView()
        {
			Title = "Splashscreen";
        }


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.InitializeAsync();
		}
    }
}

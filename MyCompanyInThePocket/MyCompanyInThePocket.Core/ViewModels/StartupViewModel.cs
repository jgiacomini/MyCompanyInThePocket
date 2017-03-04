using MvvmCross.Core.ViewModels;
namespace MyCompanyInThePocket.Core.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
		public StartupViewModel()
		{
			GoToMainPageCommand = new MvxCommand(GoToMainPage);
		}

		public IMvxCommand GoToMainPageCommand
		{
			get; private set;
		}

		private void GoToMainPage()
		{
			ApplicationSettings.LaunchStartupScreen = false;
			ShowViewModel<MainScreenViewModel> ();
		}
	}
}

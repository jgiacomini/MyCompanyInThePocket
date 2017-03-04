using MvvmCross.Core.ViewModels;
namespace MyCompanyInThePocket.Core.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
		public StartupViewModel()
		{
			GoToNextPageCommand = new MvxCommand(GoToNextPage);
		}

		public IMvxCommand GoToNextPageCommand
		{
			get; private set;
		}

		private void GoToNextPage()
		{
			ApplicationSettings.LaunchStartupScreen = false;
			ShowViewModel<MainScreenViewModel> ();
		}
	}
}

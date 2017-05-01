using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
		private readonly IAuthentificationService _authenService;

		public StartupViewModel(IAuthentificationService authenService)
		{
			_authenService = authenService;
			GoToNextPageCommand = new MvxAsyncCommand(GoToNextPageAsync);
		}

		public IMvxCommand GoToNextPageCommand
		{
			get; private set;
		}

		private async Task GoToNextPageAsync()
		{
			try
			{
				await _authenService.AuthenticateAsync();
				ApplicationSettings.LaunchStartupScreen = false;
				ShowViewModel<MainScreenViewModel>();
			}
			catch (System.Exception ex)
			{

			}
			finally
			{
				
			}
		}
	}
}

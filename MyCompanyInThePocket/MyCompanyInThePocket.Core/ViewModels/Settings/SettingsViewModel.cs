using MyCompanyInThePocket.Core.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IAuthentificationService _authenService;

		public SettingsViewModel()
		{
            _authenService = App.Instance.GetInstance<IAuthentificationService>();
			LogOutCommand = new RelayCommand(LogOutAction);
            ReviewIntroCommand = new RelayCommand(() => ShowViewModel<StartupViewModel>());
        }

		public ICommand LogOutCommand
		{
			get;
			private set;
		}
        public ICommand ReviewIntroCommand { get; private set; }

        void LogOutAction()
		{
			_authenService.Disconnect();
			ApplicationSettings.Clear();
			this.ShowViewModel<StartupViewModel>();
		}


	}
}

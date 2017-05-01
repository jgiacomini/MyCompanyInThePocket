using System;
using MvvmCross.Core.ViewModels;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IAuthentificationService _authenService;

		public SettingsViewModel(IAuthentificationService authenService)
		{
			_authenService = authenService;
			LogOutCommand = new MvxCommand(LogOutAction);
		}

		public MvxCommand LogOutCommand
		{
			get;
			private set;
		}

		void LogOutAction()
		{
			_authenService.Disconnect();
			ApplicationSettings.Clear();
			this.ShowViewModel<StartupViewModel>();
		}

	}
}

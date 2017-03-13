using System;
using MvvmCross.Core.ViewModels;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public SettingsViewModel()
		{
			LogOutCommand = new MvxCommand(LogOutAction);
		}

		public MvxCommand LogOutCommand
		{
			get;
			private set;
		}

		void LogOutAction()
		{
			ApplicationSettings.Clear();
			this.ShowViewModel<StartupViewModel>();
		}

	}
}

using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IAuthentificationService _authenService;

		public SettingsViewModel(IAuthentificationService authenService)
		{
			_authenService = authenService;
			SettingsList = new SuspendableObservableCollection<GroupedSettingsViewModel>();
			SettingsList.Add(new GroupedSettingsViewModel("Notification", new List<SettingViewModel>
			{
				new SettingViewModel("Notifications")
			}));
			SettingsList.Add(new GroupedSettingsViewModel("Compte", new List<SettingViewModel>
			{
				new SettingViewModel("Déconnexion")
			}));


			LogOutCommand = new MvxCommand(LogOutAction);
		}

		public SuspendableObservableCollection<GroupedSettingsViewModel> SettingsList { get; private set; }




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

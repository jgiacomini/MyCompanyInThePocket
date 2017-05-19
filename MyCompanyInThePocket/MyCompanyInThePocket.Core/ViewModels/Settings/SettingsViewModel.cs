using System;
using System.Collections.Generic;
using System.Windows.Input;
using MyCompanyInThePocket.Core.Services;
using GalaSoft.MvvmLight.Command;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IAuthentificationService _authenService;

        public SuspendableObservableCollection<GroupedSettingsViewModel> SettingsList { get; private set; }

		public SettingsViewModel()
		{
            _authenService = App.Instance.GetInstance<IAuthentificationService>();
			SettingsList = new SuspendableObservableCollection<GroupedSettingsViewModel>();
			SettingsList.Add(new GroupedSettingsViewModel("Notification", new List<SettingViewModel>
			{
				new SettingViewModel("Notifications")
			}));
			SettingsList.Add(new GroupedSettingsViewModel("Compte", new List<SettingViewModel>
			{
                new ButtonSettingViewModel("Déconnexion", new RelayCommand(LogOutAction))
			}));
		}

		private void LogOutAction()
		{
			_authenService.Disconnect();
			ApplicationSettings.Clear();
			this.ShowViewModel<StartupViewModel>();
		}
	}
}

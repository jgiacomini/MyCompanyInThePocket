using MyCompanyInThePocket.Core.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IAuthentificationService _authenService;
        private readonly IBackgroundTaskService _backgroundTaskService;

		public SettingsViewModel()
		{
            _authenService = App.Instance.GetInstance<IAuthentificationService>();
            _backgroundTaskService = App.Instance.BackgroundTaskService;
			LogOutCommand = new RelayCommand(LogOutAction);
        }

		public ICommand LogOutCommand
		{
			get;
			private set;
		}


        public bool IsIntegrationToCalendarEnabled
        {
            get
            {
                return ApplicationSettings.IsIntegrationToCalendarEnabled;
            }
            set
            {
                if(value != ApplicationSettings.IsIntegrationToCalendarEnabled)
                {
                    ApplicationSettings.IsIntegrationToCalendarEnabled = value;
                    RaisePropertyChanged(nameof(IsIntegrationToCalendarEnabled));
                }
            }
        }

		public bool IsIntegrationToReminderEnabled
		{
			get
			{
                return ApplicationSettings.IsIntegrationToReminderEnabled;
			}
			set
			{
				if (value != ApplicationSettings.IsIntegrationToCalendarEnabled)
				{
					ApplicationSettings.IsIntegrationToCalendarEnabled = value;

                    RaisePropertyChanged(nameof(IsIntegrationToReminderEnabled));
				}
			}
		}

		public int DelayBetweenTwoBackgroundUpdate
		{
			get
			{
                return ApplicationSettings.DelayBetweenTwoBackgroundUpdate;
			}
			set
			{
				if (value != ApplicationSettings.DelayBetweenTwoBackgroundUpdate)
				{
					ApplicationSettings.DelayBetweenTwoBackgroundUpdate = value;

                    if (IsBackgroundUpdateEnabled)
                    {
                        _backgroundTaskService.Register(ApplicationSettings.DelayBetweenTwoBackgroundUpdate);
                    }

					RaisePropertyChanged(nameof(DelayBetweenTwoBackgroundUpdate));
				}
			}
		}

		public bool IsBackgroundUpdateEnabled
		{
			get
			{
				return ApplicationSettings.IsBackgroundUpdateEnabled;
			}
			set
			{
				if (value != ApplicationSettings.IsBackgroundUpdateEnabled)
				{
					ApplicationSettings.IsBackgroundUpdateEnabled = value;

                    if (value)
                    {
                        _backgroundTaskService.Register(ApplicationSettings.DelayBetweenTwoBackgroundUpdate);
                    }
                    else
                    {
                        _backgroundTaskService.UnRegister();
                    }
					RaisePropertyChanged(nameof(IsBackgroundUpdateEnabled));
				}
			}
		}

		void LogOutAction()
		{
            _backgroundTaskService.UnRegister();
			_authenService.Disconnect();
			ApplicationSettings.Clear();
			this.ShowViewModel<StartupViewModel>();
		}
	}
}

using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyCompanyInThePocket.Core
{
	internal static class ApplicationSettings
	{
        private static readonly int _DEFAULT_TIME_BETWEEN_TWO_UPDATE = 30;

		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

        public static bool IsIntegrationToCalendarEnabled
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(IsIntegrationToCalendarEnabled), true);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(IsIntegrationToCalendarEnabled), value);
            }
        }

		public static bool IsIntegrationToReminderEnabled
		{
			get
			{
				return AppSettings.GetValueOrDefault(nameof(IsIntegrationToReminderEnabled), true);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nameof(IsIntegrationToReminderEnabled), value);
			}
		}


        public static int DelayBetweenTwoBackgroundUpdate
		{
			get
			{
				return AppSettings.GetValueOrDefault(nameof(DelayBetweenTwoBackgroundUpdate), _DEFAULT_TIME_BETWEEN_TWO_UPDATE);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nameof(DelayBetweenTwoBackgroundUpdate), value);
			}
		}

		public static bool IsBackgroundUpdateEnabled
		{
			get
			{
				return AppSettings.GetValueOrDefault(nameof(IsBackgroundUpdateEnabled), true);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nameof(IsBackgroundUpdateEnabled), value);
			}
		}


		public static bool LaunchStartupScreen
		{
			get
			{
				return AppSettings.GetValueOrDefault<bool>(nameof(LaunchStartupScreen), true);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nameof(LaunchStartupScreen), value);
			}
		}

		public static DateTime LastACRARefresh
		{
			get
			{
				return AppSettings.GetValueOrDefault<DateTime>(nameof(LaunchStartupScreen), DateTime.MinValue);
			}
			set
			{
				AppSettings.AddOrUpdateValue(nameof(LaunchStartupScreen), value);
			}
		}

		public static void Clear()
		{
            DelayBetweenTwoBackgroundUpdate = _DEFAULT_TIME_BETWEEN_TWO_UPDATE;
			IsIntegrationToCalendarEnabled = false;
			IsIntegrationToReminderEnabled = false;
			LaunchStartupScreen = false;
			OnlineSettings.Clear();
		}
	}
}

using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyCompanyInThePocket.Core
{
	internal static class ApplicationSettings
	{

		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
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
			LaunchStartupScreen = false;
			OnlineSettings.Clear();
		}
	}
}

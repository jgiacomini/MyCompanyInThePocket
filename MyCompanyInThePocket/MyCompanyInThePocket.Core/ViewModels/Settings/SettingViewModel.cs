using System;
namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
	public class SettingViewModel
	{
		public SettingViewModel(string label)
		{
			Label = label;
		}

		public string Label { get; private set; }
	}
}

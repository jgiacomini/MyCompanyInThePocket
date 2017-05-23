using System;
using System.Collections.Generic;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
	public class GroupedSettingsViewModel : List<SettingViewModel>
	{
		public GroupedSettingsViewModel(string section, IEnumerable<SettingViewModel> items)
			: base(items)
		{
			Section = section;
		}

		public string Section { get; private set; }
	}
}

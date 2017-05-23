using System;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
    public class ToggleSettingViewModel : SettingViewModel
	{
		public ToggleSettingViewModel(string label, ICommand toggleCommand, bool isOn = false)
			: base(label)
		{
			ToggleCommand = toggleCommand;
            IsOn = isOn;
		}

        public ICommand ToggleCommand { get; private set; }

        public bool IsOn { get; set; }
    }
}

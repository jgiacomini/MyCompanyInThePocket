using System;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
    public class ButtonSettingViewModel : SettingViewModel
    {
        public ButtonSettingViewModel(string label, ICommand tapCommand)
            : base(label)
        {
            TapCommand = tapCommand;
		}

        public ICommand TapCommand { get; private set; }
	}
}

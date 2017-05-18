using System;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels.Settings
{
    public class ButtonSettingViewModel : SettingViewModel
    {
        public ButtonSettingViewModel(string label, ICommand command)
            : base(label)
        {
            Command = command;
		}

        public ICommand Command { get; private set; }
	}
}

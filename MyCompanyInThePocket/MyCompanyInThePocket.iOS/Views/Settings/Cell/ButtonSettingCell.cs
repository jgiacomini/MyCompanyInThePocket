using System;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
    public class ButtonSettingCell : SettingCell
	{
		public ButtonSettingCell(IntPtr handle)
            : base(handle)
        {
		}

		public ButtonSettingCell()
            : base()
        {
		}

        public override void OnApplyBinding(SettingViewModel vm)
        {
            base.OnApplyBinding(vm);
            // TODO: bind tap command
        }
	}
}

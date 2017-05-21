using System;
using System.Windows.Input;
using Foundation;
using UIKit;
using GalaSoft.MvvmLight.Helpers;


namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
    public class ButtonSettingsCell : SettingsCell
    {
		private UIButton _button;
        public static NSString Key = new NSString("ButtonSettings");

		public ButtonSettingsCell(IntPtr handle)
        : base(handle)
        {
		}

        public ButtonSettingsCell() : 
        base(Key)
        {
        }

        protected override UIView GetView()
        {
			_button = UIButton.FromType(UIButtonType.System);
			_button.BackgroundColor = ApplicationColors.CellBackgroundColor;
            return _button;
		}

        public void OnApplyBinding(string text, ICommand command, UIColor color)
        {
			_button.SetTitleColor(color, UIControlState.Normal);
			_button.SetTitle(text, UIControlState.Normal);
            _button.SetCommand(command);
		}
    }
}

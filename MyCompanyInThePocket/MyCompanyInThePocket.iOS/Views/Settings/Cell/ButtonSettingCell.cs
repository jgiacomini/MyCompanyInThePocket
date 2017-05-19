using System;
using UIKit;
using System.Windows.Input;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using Foundation;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
    public class ButtonSettingCell : SettingCell
	{
        public new static readonly NSString Key = new NSString("ButtonSettingCell");

		private ICommand _tapCommand;

		public ButtonSettingCell(IntPtr handle)
            : base(handle)
        {
            
        }

		public ButtonSettingCell()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

			var tapGestureRecognizer = new UITapGestureRecognizer(OnTapped);
			this.AddGestureRecognizer(tapGestureRecognizer);
        }

        private void OnTapped()
        {
            _tapCommand?.Execute(null);
        }

        public override void OnApplyBinding(SettingViewModel vm)
        {
            base.OnApplyBinding(vm);

            _tapCommand = ((ButtonSettingViewModel)vm).TapCommand;
        }
    }
}

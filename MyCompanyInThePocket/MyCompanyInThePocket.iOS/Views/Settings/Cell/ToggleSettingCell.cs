using System;
using System.Windows.Input;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
    public class ToggleSettingCell : SettingCell
	{
		public new static readonly NSString Key = new NSString("ToggleSettingCell");

        private ICommand _toggleCommand;

		public ToggleSettingCell(IntPtr handle)
			: base(handle)
        {
        }

        public ToggleSettingCell()
        {
		}

		protected override void Initialize()
		{
			base.Initialize();

            ToggleButton = new UISwitch();
            AddSubview(ToggleButton);
            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            this.AddConstraints(ToggleButton.AtRightOf(this, 15));
            this.AddConstraints(ToggleButton.WithSameCenterY(this));
		}

        private void OnToggled(object sender, EventArgs e)
		{
            _toggleCommand?.Execute(((UISwitch)sender).On);
		}

		public override void OnApplyBinding(SettingViewModel vm)
		{
			base.OnApplyBinding(vm);

			_toggleCommand = ((ToggleSettingViewModel)vm).ToggleCommand;

			ToggleButton.ValueChanged += OnToggled;
            ToggleButton.On = ((ToggleSettingViewModel)vm).IsOn;
		}

        public UISwitch ToggleButton { get; private set; }
    }
}

using System;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
	public class SettingCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("SettingCell");

		public SettingCell(IntPtr handle)
		    : base(handle)
		{
            Initialize();
		}

        public SettingCell()
            : base(UITableViewCellStyle.Default, Key)
        {
            Initialize();
        }

        void Initialize()
		{
			SettingView = new SettingView();
			AddSubview(SettingView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(SettingView.FullHeightOf(this, 2));
			this.AddConstraints(SettingView.WithSameWidth(this));
        }

        public void OnApplyBinding(SettingViewModel vm)
        {
            SettingView.Label.Text = vm.Label;
        }

		public SettingView SettingView { get; private set; }
	}
}

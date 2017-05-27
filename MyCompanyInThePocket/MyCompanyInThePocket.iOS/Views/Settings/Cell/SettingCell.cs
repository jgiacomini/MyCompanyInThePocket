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

        protected virtual void Initialize()
		{
			SettingView = new SettingView();
			AddSubview(SettingView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(SettingView.FullHeightOf(this));
			this.AddConstraints(SettingView.WithSameWidth(this));
        }

        public virtual void OnApplyBinding(SettingViewModel vm)
        {
			SettingView.HorizontalAlignment = vm.HorizontalAlignment;
            SettingView.Label.Text = vm.Label;
            SettingView.Label.TextColor = vm.ColorStyle.ToTextColor();
        }

		public SettingView SettingView { get; private set; }
	}
}

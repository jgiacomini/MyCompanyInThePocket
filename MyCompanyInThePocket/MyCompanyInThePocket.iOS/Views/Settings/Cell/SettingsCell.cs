using System;
using Foundation;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
    public abstract class SettingsCell : UITableViewCell
	{
		private UIView _topSeparatorView;
		private UIView _bottomSeparatorView;
     
		public SettingsCell(IntPtr handle)
		: base(handle)
		{
            Initialize();
		}

        public SettingsCell(NSString key)
            : base(UITableViewCellStyle.Default, key)
        {
            Initialize();
        }

        protected abstract UIView GetView();
       

        protected void Initialize()
        {
			_topSeparatorView = new UIView();
			_topSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			_bottomSeparatorView = new UIView();
			_bottomSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

            var centerView = GetView();
           	
            nfloat separatorHeight = 1;
			nfloat vmargin = 15;

			this.AddSubviews(_topSeparatorView, centerView, _bottomSeparatorView);
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			this.AddConstraints(
			_topSeparatorView.WithSameWidth(this),
			_topSeparatorView.Height().EqualTo(separatorHeight),
			_topSeparatorView.AtTopOf(this, vmargin),
			centerView.WithSameWidth(this),
			centerView.Below(_topSeparatorView),
			_bottomSeparatorView.WithSameWidth(this),
			_bottomSeparatorView.Height().EqualTo(separatorHeight),
			_bottomSeparatorView.Below(centerView));
		}
	}
}

using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.iOS.Views
{
	public class SettingsScreen : BaseScreen<SettingsViewModel>
	{
		private UIButton _logoutButton;
		private UIView _topSeparatorView;
		private UIView _bottomSeparatorView;
		public SettingsScreen()
		{
			View.BackgroundColor = ApplicationColors.BackgroundColor;
		}

		public override void ViewDidLoad()
		{
			_topSeparatorView = new UIView();
			_topSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			_logoutButton = UIButton.FromType(UIButtonType.System);
			_logoutButton.SetTitle(StringValues.Main_Settings_Logout, UIControlState.Normal);
			_logoutButton.SetTitleColor(UIColor.Red, UIControlState.Normal);
			_logoutButton.BackgroundColor = ApplicationColors.CellBackgroundColor;

			_bottomSeparatorView = new UIView();
			_bottomSeparatorView.BackgroundColor = ApplicationColors.SeparatorColor;

			View.AddSubviews(_topSeparatorView, _logoutButton, _bottomSeparatorView);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
			nfloat separatorHeight = 1;
			nfloat vmargin = 15;
			View.AddConstraints(
				_topSeparatorView.WithSameWidth(View),
				_topSeparatorView.Height().EqualTo(separatorHeight),
				_topSeparatorView.AtTopOf(View, vmargin),
				_logoutButton.WithSameWidth(View),
				_logoutButton.Below(_topSeparatorView),
				_bottomSeparatorView.WithSameWidth(View),
				_bottomSeparatorView.Height().EqualTo(separatorHeight),
				_bottomSeparatorView.Below(_logoutButton));

			var set = this.CreateBindingSet<SettingsScreen, SettingsViewModel>();
			set.Bind(_logoutButton).To(vm => vm.LogOutCommand);
			set.Apply();

			base.ViewDidLoad();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}
	}
}

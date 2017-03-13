using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	public class SettingsScreen : BaseScreen<SettingsViewModel>
	{
		private UIButton _logoutButton;
		public SettingsScreen()
		{
		}

		public override void ViewDidLoad()
		{
			_logoutButton = UIButton.FromType(UIButtonType.System);
			_logoutButton.SetTitle("Déconnexion", UIControlState.Normal);
			_logoutButton.SetTitleColor(UIColor.Red, UIControlState.Normal);
			View.AddSubview(_logoutButton);

			View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			View.AddConstraints(_logoutButton.WithSameWidth(View));

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

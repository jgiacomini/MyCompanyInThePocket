using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	public abstract class BaseTabBarScreen<TViewModel> : MvxTabBarViewController<TViewModel> 
		where TViewModel : class, IMvxViewModel
	{
		public BaseTabBarScreen()
		{
			View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
			ViewDidLoad();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ScreenHelper.RemoveNoHistoryPage(NavigationController, this);
		}
	}
}

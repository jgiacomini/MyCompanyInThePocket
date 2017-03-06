using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;
using System.Linq;

namespace MyCompanyInThePocket.iOS.Views
{
	public abstract class BaseScreen<TViewModel> : MvxViewController<TViewModel>, INoHistoryScreen
		where TViewModel : class, IMvxViewModel
	{

		public BaseScreen()
		{
			View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ScreenHelper.RemoveNoHistoryPage(NavigationController, this);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}
	}
}

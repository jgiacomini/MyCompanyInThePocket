using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;
using System.Linq;

namespace MyCompanyInThePocket.iOS.Views
{
	public class BaseScreen<TViewModel> : MvxViewController<TViewModel>, IBaseScreen
		where TViewModel : class, IMvxViewModel
	{

		public bool NoHistory
		{
			get;
			set;
		}

		public BaseScreen()
		{
			View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			var controllers = NavigationController.ViewControllers.OfType<IBaseScreen>().ToList();
			foreach (var item in controllers)
			{
				if (item.NoHistory && item.GetType() != this.GetType())
				{
					(item as UIViewController).RemoveFromParentViewController();
				}
			}
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}
	}
}

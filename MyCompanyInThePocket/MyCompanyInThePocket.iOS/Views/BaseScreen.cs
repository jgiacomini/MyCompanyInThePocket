using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	public class BaseScreen<TViewModel> : MvxViewController<TViewModel>
		where TViewModel : class, IMvxViewModel
	{
		public BaseScreen()
		{
			View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
		}
	}
}

using MvvmCross.iOS.Views;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class BaseViewController : MvxViewController
	{
		public BaseViewController()
		{
			CurrentViewController.Current = this;
		}
	}

	public static class CurrentViewController
	{
		public static UIViewController Current { get; set; }
	}
}

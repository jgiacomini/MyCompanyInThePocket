using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public static class CurrentViewController
	{
		public static UIViewController Current { get; set; }
	}
}
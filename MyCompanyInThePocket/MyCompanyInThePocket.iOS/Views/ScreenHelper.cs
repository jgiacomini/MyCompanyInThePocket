using System;
using UIKit;
using System.Linq;

namespace MyCompanyInThePocket.iOS.Views
{
	public static class ScreenHelper
	{
		public static void RemoveNoHistoryPage(UINavigationController navigationController, UIViewController currentViewController)
		{ 
			var controllers = navigationController.ViewControllers.OfType<INoHistoryScreen>().ToList();
			foreach (var item in controllers)
			{
				if (item != currentViewController)
				{
					(item as UIViewController).RemoveFromParentViewController();
				}
			}
		}

		public static void ClearAllHistory(UINavigationController navigationController, UIViewController currentViewController)
		{ 
			var controllers = navigationController.ViewControllers.ToList();
			foreach (var item in controllers)
			{
				if (item != currentViewController)
				{
					(item as UIViewController).RemoveFromParentViewController();
				}
			}
		}
	}
}

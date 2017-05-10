using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
    public abstract class BaseTabBarViewController<TViewModel> : UITabBarController
		where TViewModel : BaseViewModel
	{
		public BaseTabBarViewController(TViewModel viewModel)
        {
            ViewModel = viewModel;
            View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
			ViewDidLoad();
        }

        public TViewModel ViewModel { get; private set; }
        
        public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ScreenHelper.RemoveNoHistoryPage(NavigationController, this);
		}
	}
}

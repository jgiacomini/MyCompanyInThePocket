using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : UIViewController, INoHistoryScreen
		where TViewModel : BaseViewModel
	{

		public BaseViewController(TViewModel viewModel)
		{
            ViewModel = viewModel;

            View.BackgroundColor = UIColor.White;
			EdgesForExtendedLayout = UIRectEdge.None;
			CurrentViewController.Current = this;
		}

        public TViewModel ViewModel { get; private set; }

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

using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
    public abstract class BaseViewController<TViewModel> : UIViewController, INoHistoryScreen
		where TViewModel : BaseViewModel , new()
	{
        private TViewModel _viewModel;

		public BaseViewController()
		{

            View.BackgroundColor = UIColor.White;
			EdgesForExtendedLayout = UIRectEdge.None;
			CurrentViewController.Current = this;
		}

        public TViewModel ViewModel 
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new TViewModel();
                }

                return _viewModel;
            }
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

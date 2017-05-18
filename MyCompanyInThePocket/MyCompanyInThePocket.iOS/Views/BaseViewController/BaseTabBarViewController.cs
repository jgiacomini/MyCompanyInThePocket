using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
    public abstract class BaseTabBarViewController<TViewModel> : UITabBarController
		where TViewModel : BaseViewModel, new()
	{
        private TViewModel _viewModel;

		public BaseTabBarViewController()
        {
            View.BackgroundColor = UIColor.White;
			CurrentViewController.Current = this;
			ViewDidLoad();
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
	}
}

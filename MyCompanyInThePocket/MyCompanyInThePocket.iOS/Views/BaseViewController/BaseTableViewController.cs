using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.BaseViewController
{
    public class BaseTableViewController<TViewModel> : UITableViewController
        where TViewModel : BaseViewModel, new()
    {
        private TViewModel _viewModel;

        public BaseTableViewController()
        {
            View.BackgroundColor = UIColor.White;
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
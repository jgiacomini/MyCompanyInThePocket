using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.BaseViewController
{
    public class BaseTableViewController<TViewModel> : UITableViewController
        where TViewModel : BaseViewModel
    {
        private TViewModel _viewModel;

        public BaseTableViewController(TViewModel viewModel)
        {
            _viewModel = viewModel;
            View.BackgroundColor = UIColor.White;
            ViewDidLoad();
        }

        public TViewModel ViewModel
        {
            get;
            private set;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ScreenHelper.RemoveNoHistoryPage(NavigationController, this);
        }
    }
}
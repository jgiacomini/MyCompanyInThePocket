using MyCompanyInThePocket.Core.ViewModels;
using UIKit;
using System.ComponentModel;
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
                    _viewModel.PropertyChanged += _viewModel_PropertyChanged;
				}

				return _viewModel;
			}
		}


        protected virtual void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ScreenHelper.RemoveNoHistoryPage(NavigationController, this);
        }
    }
}
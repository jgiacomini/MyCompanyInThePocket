using MyCompanyInThePocket.Core.ViewModels;
using UIKit;
namespace MyCompanyInThePocket.iOS.Views
{
	public class MeetingsViewController : BaseTableViewController<MeetingsViewModel>
	{
        public MeetingsViewController() : base(false)
		{
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 50;
			TableView.AllowsSelection = false;	
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

        /*
        private ToastView _toastView;

		public ToastView ToastView
		{
            get{

                if (_toastView == null)
                {
                    _toastView = new ToastView();
                }

                return _toastView;
            }
		}*/

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//ToastView.Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, 20);
            //TableView.TableHeaderView = ToastView;
			var refreshControl = new BindableUIRefreshControl();
			RefreshControl = refreshControl;
			var source = new MeetingsViewSource(TableView, ViewModel.Meetings);
			TableView.Source = source;
            refreshControl.RefreshCommand = ViewModel.RefreshCommand;
            refreshControl.Message = ViewModel.LastUpdate;
            refreshControl.IsRefreshing = ViewModel.IsBusy;

			// Load data
			TableView.ReloadData();
		}

        protected override void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs arg)
        {
            var refreshControl = RefreshControl as BindableUIRefreshControl;

            if (refreshControl != null)
            {
                if (arg.PropertyName == nameof(ViewModel.IsBusy))
                {
                    refreshControl.IsRefreshing = ViewModel.IsBusy;
                }
                if (arg.PropertyName == nameof(ViewModel.LastUpdate))
                {
                    refreshControl.Message = ViewModel.LastUpdate;
                }
            }
		}

        public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			await ViewModel.InitializeAsync();
			//ToastView.Show();

			TableView.ReloadData();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			ViewModel.CancelQueries();
		}

	}
}

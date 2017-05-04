using MvvmCross.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	public class MeetingsScreen : MvxTableViewController<MeetingsViewModel>
	{
		public MeetingsScreen()
		{
			ToastView = new ToastView();

			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 50;
			TableView.AllowsSelection = false;
			TableView.TableHeaderView = ToastView;
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		public ToastView ToastView
		{
			get;
			set;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ToastView.Frame = new CoreGraphics.CGRect(0, 0, View.Bounds.Width, 20);

			var refreshControl = new MvxUIRefreshControl();
			RefreshControl = refreshControl;

			var source = new MeetingsViewSource(TableView);
			TableView.Source = source;

			var set = this.CreateBindingSet<MeetingsScreen, MeetingsViewModel>();
			set.Bind(source).To(vm => vm.Meetings);
			set.Bind(refreshControl).For(r => r.Message).To(vm => vm.LastUpdate);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Apply();

			// Load data
			TableView.ReloadData();
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			await ViewModel.InitializeAsync();
			ToastView.Show();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			ViewModel.CancelQueries();
		}

	}
}

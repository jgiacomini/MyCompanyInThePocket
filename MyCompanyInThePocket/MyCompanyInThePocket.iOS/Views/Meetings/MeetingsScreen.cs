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
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 50;
			TableView.AllowsSelection = false;
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var refreshControl = new MvxUIRefreshControl();
			this.RefreshControl = refreshControl;
			var source = new MeetingsViewSource(TableView);
			TableView.Source = source;

			var set = this.CreateBindingSet<MeetingsScreen, MeetingsViewModel>();
			set.Bind(source).To(vm => vm.Meetings);
			set.Bind(refreshControl).For(r => r.Message).To(vm => vm.LastUpdate);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);

			set.Apply();

			await ViewModel.InitializeAsync();
			// Load data
			TableView.ReloadData();
		}

	}
}

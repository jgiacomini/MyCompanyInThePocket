using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmCross.Binding.iOS.Views;
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
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TableView.RowHeight = 50;

			//this.ViewModel.ReloadCommand
			var source = new MeetingsViewSource(TableView, ViewModel.Meetings);
			TableView.Source = source;

			var set = this.CreateBindingSet<MeetingsScreen, MeetingsViewModel>();
			set.Bind(source).To(vm => vm.Meetings);
			/*set.Bind(_refreshControl).For(r => r.Message).To(vm => vm.TraysUpdateTime);
			set.Bind(_refreshControl).For(r => r.RefreshCommand).To(vm => vm.ReloadCommand);
			set.Bind(_refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			*/
			set.Apply();

			await ViewModel.InitializeAsync();
			// Load data
			TableView.ReloadData();
		}


		public class MeetingsViewSource : MvxTableViewSource
		{
			private readonly ObservableCollection<MeetingViewModel> _meetings;

			public MeetingsViewSource(UITableView tableView, ObservableCollection<MeetingViewModel> meetings)
				: base(tableView)
			{
				_meetings = meetings;
				tableView.RegisterClassForCellReuse(typeof(MeetingCell), MeetingCell.Key);
			}

			protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
			{
				return (UITableViewCell)TableView.DequeueReusableCell(MeetingCell.Key, indexPath);
			}
		}
	}
}

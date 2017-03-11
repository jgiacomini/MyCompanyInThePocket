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
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TableView.RowHeight = 50;
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


		public class MeetingsViewSource : MvxTableViewSource
		{

			public MeetingsViewSource(UITableView tableView)
				: base(tableView)
			{
				tableView.RegisterClassForCellReuse(typeof(MeetingCell), MeetingCell.Key);
			}

			protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
			{
				return (UITableViewCell)TableView.DequeueReusableCell(MeetingCell.Key, indexPath);
			}


			public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
			{
				// On désactive la couleur blanche de fond de cellule
				cell.BackgroundColor = UIColor.Clear;
				cell.BackgroundView = null;
			}
		}
	}
}

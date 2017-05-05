using System;
using System.Linq;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using MyCompanyInThePocket.iOS.Views.Settings.Cell;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings
{
	public class SettingsViewSource : MvxTableViewSource
	{
		private SuspendableObservableCollection<GroupedSettingsViewModel> GetGroupedData()
		{
			var source = ItemsSource as SuspendableObservableCollection<GroupedSettingsViewModel>;

			if (source == null)
			{
				return new SuspendableObservableCollection<GroupedSettingsViewModel>();
			}

			return source;
		}

		public SettingsViewSource(UITableView tableView)
			: base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(SettingCell), SettingCell.Key);
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			var groupedData = GetGroupedData();

			if (groupedData.Any())
			{
				var group = groupedData[(int)section];
				return group.Count;
			}

			return 0;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			var count = GetGroupedData().Count;

			return count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			var groupedData = GetGroupedData();

			return !groupedData.Any() ? string.Empty : groupedData[(int)section].Section;
		}

		public override nfloat EstimatedHeightForHeader(UITableView tableView, nint section)
		{
			return 20;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 20;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var groupedData = GetGroupedData();
			return new SettingCellHeaderView(groupedData[(int)section]);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return (UITableViewCell)TableView.DequeueReusableCell(SettingCell.Key, indexPath);
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			var groupedData = GetGroupedData();

			if (!groupedData.Any())
			{
				return null;
			}

			var group = groupedData[indexPath.Section];
			return group[indexPath.Row];
		}

		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			// On désactive la couleur blanche de fond de cellule
			cell.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = null;
		}
	}
}

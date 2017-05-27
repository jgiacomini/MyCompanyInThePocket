using System;
using System.Linq;
using Foundation;
using MyCompanyInThePocket.Core.ViewModels;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using MyCompanyInThePocket.iOS.Views.Settings.Cell;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings
{
	public class SettingsViewSource : UITableViewSource
	{
		public SettingsViewSource(UITableView tableView, SuspendableObservableCollection<GroupedSettingsViewModel> itemsSource)
		{
			tableView.RegisterClassForCellReuse(typeof(SettingCell), SettingCell.Key);
			tableView.RegisterClassForCellReuse(typeof(ButtonSettingCell), ButtonSettingCell.Key);
			tableView.RegisterClassForCellReuse(typeof(ToggleSettingCell), ToggleSettingCell.Key);
			ItemsSource = itemsSource;
		}

		public SuspendableObservableCollection<GroupedSettingsViewModel> ItemsSource { get; private set; }

		private SuspendableObservableCollection<GroupedSettingsViewModel> GetGroupedData()
		{
			var source = ItemsSource as SuspendableObservableCollection<GroupedSettingsViewModel>;

			if (source == null)
			{
				return new SuspendableObservableCollection<GroupedSettingsViewModel>();
			}

			return source;
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
			return 45;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 45;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var groupedData = GetGroupedData();
			return new SettingCellHeaderView(groupedData[(int)section]);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{

            bool isFirstCell;
			bool isLastCell;

            var vm = GetItemAt(indexPath, out isFirstCell, out isLastCell);

			SettingCell cell;

			if (vm.GetType() == typeof(ButtonSettingViewModel))
			{
				cell = tableView.DequeueReusableCell(ButtonSettingCell.Key, indexPath) as ButtonSettingCell;
				if (cell == null)
				{
					cell = new ButtonSettingCell();
				}
			}
			else if (vm.GetType() == typeof(ToggleSettingViewModel))
			{
				cell = tableView.DequeueReusableCell(ToggleSettingCell.Key, indexPath) as ToggleSettingCell;
				if (cell == null)
				{
					cell = new ToggleSettingCell();
				}
			}
			else
			{
				cell = tableView.DequeueReusableCell(SettingCell.Key, indexPath) as SettingCell;
				if (cell == null)
				{
					cell = new SettingCell();
				}
			}

            cell.SettingView.HaveFullTopSeparator = isFirstCell;
            cell.SettingView.HaveFullBottomSeparator = isLastCell;
			cell.OnApplyBinding(vm);
			cell.SettingView.CreateConstraints();
			return cell;
		}

		protected SettingViewModel GetItemAt(NSIndexPath indexPath, out bool isFirstCell, out bool isLastCell)
		{
			var groupedData = GetGroupedData();

			if (!groupedData.Any())
			{
                isFirstCell = true;
                isLastCell = true;
				return null;
			}

			var groupData = groupedData[indexPath.Section];
            if (indexPath.Row == 0)
            {
                isFirstCell = true;
            }
            else
            {
                isFirstCell = false;
            }

            if (indexPath.Row == groupData.Count -1)
            {
                isLastCell = true;
            }
			else
			{
				isLastCell = false;
			}
			
            return groupData[indexPath.Row];
		}

		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			// On désactive la couleur blanche de fond de cellule
			cell.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = null;
		}
	}
}
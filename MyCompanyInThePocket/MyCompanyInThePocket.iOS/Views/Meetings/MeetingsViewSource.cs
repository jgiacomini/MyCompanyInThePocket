using System;
using MyCompanyInThePocket.Core.ViewModels;
using UIKit;
using System.Linq;
using Foundation;

namespace MyCompanyInThePocket.iOS.Views
{
	public class MeetingsViewSource : UITableViewSource
	{

        public MeetingsViewSource(UITableView tableView, SuspendableObservableCollection<GroupedMeetingViewModel> itemsSource)
        {
            tableView.RegisterClassForCellReuse(typeof(MeetingCell), MeetingCell.Key);
            ItemsSource = itemsSource;
        }

        public SuspendableObservableCollection<GroupedMeetingViewModel> ItemsSource { get; private set; }

        private SuspendableObservableCollection<GroupedMeetingViewModel> GetGroupedData()
		{
            var source = ItemsSource;

			if (source == null)
			{
				return new SuspendableObservableCollection<GroupedMeetingViewModel>();
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
			var count =  GetGroupedData().Count;

			return count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			var groupedData = GetGroupedData();

			return !groupedData.Any() ? string.Empty : groupedData[(int)section].Date.ToShortDateString();
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
			return new MeetingCellHeaderView(groupedData[(int)section]);
		}

		protected MeetingViewModel GetItemAt(NSIndexPath indexPath)
		{
			var groupedData = GetGroupedData();

			if (!groupedData.Any())
			{
				return null;
			}

			var group = groupedData[indexPath.Section];
			return group[indexPath.Row];
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MeetingCell.Key, indexPath) as MeetingCell;

            if (cell == null)
            {
                cell = new MeetingCell();
            }

            var vm = GetItemAt(indexPath);
            cell.OnApplyBinding(vm);
            return cell;
        }
    }
}

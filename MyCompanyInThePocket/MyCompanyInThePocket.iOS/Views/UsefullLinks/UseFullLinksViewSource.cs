using System;
using Foundation;
using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.Core.ViewModels;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinksViewSource : UITableViewSource
	{
		public UseFullLinksViewSource(UITableView tableView, SuspendableObservableCollection<UseFullLinkViewModel> itemsSource)
				: base()
		{
			tableView.RegisterClassForCellReuse(typeof(UseFullLinkCell), UseFullLinkCell.Key);
            ItemsSource = itemsSource;
		}

        public SuspendableObservableCollection<UseFullLinkViewModel> ItemsSource { get; private set; }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(UseFullLinkCell.Key, indexPath) as UseFullLinkCell;

            if (cell == null)
            {
                cell = new UseFullLinkCell();
            }

            var vm = ItemsSource[indexPath.Row];
            cell.OnApplyBinding(vm);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemsSource.Count;
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			cell.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = null;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
		}
	}
}

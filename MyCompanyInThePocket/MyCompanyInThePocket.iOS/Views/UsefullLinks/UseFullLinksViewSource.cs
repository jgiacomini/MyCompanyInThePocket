using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using MyCompanyInThePocket.Core;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinksViewSource : MvxTableViewSource
	{
		public UseFullLinksViewSource(UITableView tableView)
				: base(tableView)
		{
			tableView.RegisterClassForCellReuse(typeof(UseFullLinkCell), UseFullLinkCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return (UITableViewCell)TableView.DequeueReusableCell(UseFullLinkCell.Key, indexPath);
		}

		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			cell.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = null;
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
		}
	}
}

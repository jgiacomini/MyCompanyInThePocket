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
		private readonly ObservableCollection<UseFullLinkViewModel> _useFullLinks;

		public UseFullLinksViewSource(UITableView tableView, ObservableCollection<UseFullLinkViewModel> useFullLinks)
				: base(tableView)
		{
			_useFullLinks = useFullLinks;
			tableView.RegisterClassForCellReuse(typeof(UseFullLinkCell), UseFullLinkCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			return (UITableViewCell)TableView.DequeueReusableCell(UseFullLinkCell.Key, indexPath);
		}
	}
}

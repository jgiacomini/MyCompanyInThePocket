using System;
using MyCompanyInThePocket.Core.ViewModels;
using UIKit;
using System.Linq;
using Foundation;
using MyCompanyInThePocket.iOS.Views.Settings.Cell;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.iOS.Views
{
	public class SeetingsViewSource : UITableViewSource
	{

        private readonly SettingsViewModel _settingsVM;

        public SeetingsViewSource(UITableView tableView, SettingsViewModel settingsVM)
        {
            _settingsVM = settingsVM;
            tableView.RegisterClassForCellReuse(typeof(ButtonSettingsCell), ButtonSettingsCell.Key);
        }

       
		public override nint RowsInSection(UITableView tableview, nint section)
		{
            return 1;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			var count =  3;

			return count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
            return string.Empty;
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
            return null;
		}

		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			// On désactive la couleur blanche de fond de cellule
			cell.BackgroundColor = UIColor.Clear;
			cell.BackgroundView = null;
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(ButtonSettingsCell.Key, indexPath) as ButtonSettingsCell;

            if (cell == null)
            {
                cell = new ButtonSettingsCell();
            }

            cell.OnApplyBinding(StringValues.Main_Settings_Logout, _settingsVM.LogOutCommand, UIColor.Red);
            return cell;
        }
    }
}

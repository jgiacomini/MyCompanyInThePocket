using System;
using MyCompanyInThePocket.Core.ViewModels;
using Cirrious.FluentLayouts.Touch;
using UIKit;
using MyCompanyInThePocket.Core.Resources;
using GalaSoft.MvvmLight.Helpers;

namespace MyCompanyInThePocket.iOS.Views
{
    public class SettingsViewController : BaseTableViewController<SettingsViewModel>
    {

        public SettingsViewController()
            :base(false)
        {
            View.BackgroundColor = ApplicationColors.BackgroundColor;
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 50;
			TableView.AllowsSelection = false;
        }

        public override void ViewDidLoad()
        {
			var source = new SeetingsViewSource(TableView, ViewModel);
			TableView.Source = source;
            TableView.ReloadData();
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}

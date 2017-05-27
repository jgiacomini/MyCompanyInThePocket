using System;
using Cirrious.FluentLayouts.Touch;
using UIKit;
using MyCompanyInThePocket.Core.Resources;
using GalaSoft.MvvmLight.Helpers;
using MyCompanyInThePocket.Core.ViewModels.Settings;

namespace MyCompanyInThePocket.iOS.Views.Settings
{
	public class SettingsViewController : BaseTableViewController<SettingsViewModel>
	{
		public SettingsViewController()
            :base(false)
		{
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 40;
			TableView.AllowsSelection = false;
			TableView.ScrollEnabled = false;
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new SettingsViewSource(TableView, ViewModel.SettingsList);
			TableView.Source = source;

			// Load data
			TableView.ReloadData();
		}
	}
}
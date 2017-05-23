using System;
using MvvmCross.Binding.BindingContext;
using Cirrious.FluentLayouts.Touch;
using UIKit;
using MyCompanyInThePocket.Core.Resources;
using MvvmCross.iOS.Views;
using MyCompanyInThePocket.Core.ViewModels.Settings;

namespace MyCompanyInThePocket.iOS.Views.Settings
{
	public class SettingsScreen : MvxTableViewController<SettingsViewModel>
	{
		public SettingsScreen()
		{
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.RowHeight = 50;
			TableView.AllowsSelection = false;
			TableView.ScrollEnabled = false;
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new SettingsViewSource(TableView);
			TableView.Source = source;

			var set = this.CreateBindingSet<SettingsScreen, SettingsViewModel>();
			set.Bind(source).To(vm => vm.SettingsList);
			set.Apply();

			// Load data
			TableView.ReloadData();
		}
	}
}

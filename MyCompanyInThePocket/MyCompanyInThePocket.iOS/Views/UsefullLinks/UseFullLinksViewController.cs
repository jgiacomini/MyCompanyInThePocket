using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.iOS.Views.BaseViewController;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinksViewController : BaseTableViewController<UseFullLinksViewModel>
	{
		public UseFullLinksViewController(UseFullLinksViewModel viewModel)
           : base(viewModel)
		{
			View.BackgroundColor = ApplicationColors.BackgroundColor;
			// On désactive les barres de séparation native.
			TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TableView.RowHeight = 50;

			var source = new UseFullLinksViewSource(TableView, ViewModel.UseFullLinks);
			TableView.Source = source;
			await ViewModel.InitializeAsync();

			// Load data
			TableView.ReloadData();
		}
	}
}

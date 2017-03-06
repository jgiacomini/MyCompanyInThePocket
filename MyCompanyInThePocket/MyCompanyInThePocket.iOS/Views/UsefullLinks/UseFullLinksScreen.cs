using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MyCompanyInThePocket.Core;

namespace MyCompanyInThePocket.iOS
{
	public class UseFullLinksScreen : MvxTableViewController<UseFullLinksViewModel>
	{
		public UseFullLinksScreen()
		{
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TableView.RowHeight = 50;

			var source = new UseFullLinksViewSource(TableView, ViewModel.UseFullLinks);
			TableView.Source = source;

			var set = this.CreateBindingSet<UseFullLinksScreen, UseFullLinksViewModel>();
			set.Bind(source).To(vm => vm.UseFullLinks);
			set.Apply();

			await ViewModel.InitializeAsync();

			// Load data
			TableView.ReloadData();
		}
	}
}

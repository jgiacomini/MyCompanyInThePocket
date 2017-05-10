using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.Core.Resources;
using MyCompanyInThePocket.Core.ViewModels;
using System;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	public class MainViewController : BaseTabBarViewController<MainViewModel>
	{
		private int _nbTabBarCreated;

		public MainViewController(MainViewModel viewModel)
            :base(viewModel)
		{
			this.ViewControllerSelected += Handle_ViewControllerSelected;
			this.TabBar.Translucent = false;
			this.TabBar.TintColor = ApplicationColors.MainColor;
			this.TabBar.BackgroundColor = UIColor.White;
			this.EdgesForExtendedLayout = UIRectEdge.None;

		}

		void Handle_ViewControllerSelected(object sender, UITabBarSelectionEventArgs e)
		{
			// On définit comme titre de la page le nom de la page sélectionnée
			Title = TabBar.SelectedItem.Title;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var viewControllers = new UIViewController[]
			{
				CreateTabFor(StringValues.Main_Meetings_Title_Text, "ic_menu_meetings", ViewModel.MeetingsVM),
				CreateTabFor(StringValues.Main_UseFullLinks_Title_Text, "ic_menu_link", ViewModel.UseFullLinksVM),
				/*CreateTabFor(StringValues.Main_Meetings_Title_Text, "ic_menu_meetings", ViewModel.MeetingsVM),*/
				CreateTabFor(StringValues.Main_Settings_Title_Text, "ic_menu_settings", ViewModel.SettingsVM),
			};
			ViewControllers = viewControllers;

			if (SelectedViewController == null)
			{
				SelectedViewController = ViewControllers[0];
			}

			Title = SelectedViewController?.Title;
		}

		private UIViewController CreateTabFor(string title, string imageName, BaseViewModel viewModel)
		{
			// Création de l'écran correspondant au viewModel;
			var screen = this.CreateViewControllerFor(viewModel) as UIViewController;

			var image = UIImage.FromBundle($"TabBar/{imageName}.png");
			screen.Title = title;
			screen.TabBarItem = new UITabBarItem(title, image, _nbTabBarCreated);
			_nbTabBarCreated++;
			return screen;
		}

        private UIViewController CreateViewControllerFor(BaseViewModel vm)
        {
            if (vm is MeetingsViewModel)
            {
                return new MeetingsViewController(vm as MeetingsViewModel);
            }
            if (vm is UseFullLinksViewModel)
            {
                return new UseFullLinksViewController(vm as UseFullLinksViewModel);
            }
            if (vm is SettingsViewModel)
            {
                return new SettingsViewController(vm as SettingsViewModel);
            }

            throw new NotImplementedException($"{vm.GetType().Name} has no view controller implemented in CreateViewControllerFor");
        }

        public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			// On réaffiche la bar de navigation
			NavigationController.SetNavigationBarHidden(false, true);
		}
	}
}

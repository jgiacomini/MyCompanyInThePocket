using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.Core.Resources;
using MyCompanyInThePocket.Core.ViewModels;
using System;
using UIKit;
using MyCompanyInThePocket.iOS.Views.Settings;

namespace MyCompanyInThePocket.iOS.Views
{
	public class MainViewController : BaseTabBarViewController<MainViewModel>
	{
		private int _nbTabBarCreated;

		public MainViewController()
		{
			this.ViewControllerSelected += Handle_ViewControllerSelected;
			this.TabBar.Translucent = false;
			this.TabBar.TintColor = ApplicationColors.MainColor;
			this.TabBar.BackgroundColor = UIColor.White;
			this.EdgesForExtendedLayout = UIRectEdge.None;

            //this.TabBar.BackgroundColor = UIColor.Clear;
            //this.TabBar.TintColor = ApplicationColors.MainColor;
            //this.TabBar.Translucent = true;
            //this.ExtendedLayoutIncludesOpaqueBars = true;
            //this.EdgesForExtendedLayout = UIRectEdge.All;
            //this.AutomaticallyAdjustsScrollViewInsets = true;
            //this.ExtendedLayoutIncludesOpaqueBars = false;
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
				CreateTabFor(StringValues.Main_Meetings_Title_Text, "ic_menu_meetings"),
				CreateTabFor(StringValues.Main_UseFullLinks_Title_Text, "ic_menu_link"),
				/*CreateTabFor(StringValues.Main_Meetings_Title_Text, "ic_menu_meetings"),*/
				CreateTabFor(StringValues.Main_Settings_Title_Text, "ic_menu_settings"),
			};
			ViewControllers = viewControllers;

			if (SelectedViewController == null)
			{
				SelectedViewController = ViewControllers[0];
			}

			Title = SelectedViewController?.Title;
		}

		private UIViewController CreateTabFor(string title, string imageName)
		{
			// Création de l'écran correspondant au viewModel;
            var screen = CreateViewControllerFor(title) as UIViewController;
			var image = UIImage.FromBundle($"TabBar/{imageName}.png");
			screen.Title = title;
			screen.TabBarItem = new UITabBarItem(title, image, _nbTabBarCreated);
			_nbTabBarCreated++;
			return screen;
		}

        private UIViewController CreateViewControllerFor(string title)
        {
            if (title == StringValues.Main_Meetings_Title_Text)
            {
                return new MeetingsViewController();
            }
            else if (title == StringValues.Main_UseFullLinks_Title_Text)
			{
                return new UseFullLinksViewController();
            }
			else if (title == StringValues.Main_Settings_Title_Text)
			{
                return new SettingsViewController();
            }

            throw new NotImplementedException($"{title} has no view controller implemented in CreateViewControllerFor");
        }

        public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			// On réaffiche la bar de navigation
			NavigationController.SetNavigationBarHidden(false, true);
		}
	}
}

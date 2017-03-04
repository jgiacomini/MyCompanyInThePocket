using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.iOS.Views
{
	public class MainScreenView : BaseScreen<MainScreenViewModel>
    {
        public MainScreenView()
        {
			Title = "MainScreen";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			NavigationController.SetNavigationBarHidden(false, true);
		}
    }
}

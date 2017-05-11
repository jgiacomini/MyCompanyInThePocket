using UIKit;
using MyCompanyInThePocket.Core.ViewModels;
using CoreGraphics;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;

namespace MyCompanyInThePocket.iOS.Views
{
    public class StartupViewController : BaseViewController<StartupViewModel>
	{
        public StartupViewController()
            :base(true)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
            const int buttonHeight = 50;

            var pagerSurload = new UIPageControl
            {
                Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height - buttonHeight - buttonHeight, UIScreen.MainScreen.Bounds.Width, buttonHeight),
                Pages = 4
            };

            var pvController = new UIPageViewController(UIPageViewControllerTransitionStyle.Scroll, UIPageViewControllerNavigationOrientation.Horizontal);
            var pages = new List<StartupPageFeatureViewController>();
            int index = 0;
            pages.Add(new StartupPageFeatureViewController(
                 Core.Resources.StringValues.Startup_FlipViewItem1_Title_Text,
                 Core.Resources.StringValues.Startup_FlipViewItem1_Content_Text,
                index++));

            pages.Add(new StartupPageFeatureViewController(
               Core.Resources.StringValues.Startup_FlipViewItem2_Title_Text,
               Core.Resources.StringValues.Startup_FlipViewItem2_Content_Text,
              index++));

            pages.Add(new StartupPageFeatureViewController(
               Core.Resources.StringValues.Startup_FlipViewItem3_Title_Text,
               Core.Resources.StringValues.Startup_FlipViewItem3_Content_Text,
              index++));

            pages.Add(new StartupPageFeatureViewController(
                 Core.Resources.StringValues.Startup_FlipViewItem4_Title_Text,
                 Core.Resources.StringValues.Startup_FlipViewItem4_Content_Text,
                index++));

            pvController.DataSource = new PageDataSource(pages, pagerSurload);
            pvController.View.Frame = new CGRect(0, 0, View.Bounds.Width, View.Bounds.Height - buttonHeight);
            pvController.SetViewControllers(new UIViewController[] { pages[0], }, UIPageViewControllerNavigationDirection.Forward, false, s => { });

            AddChildViewController(pvController);
            View.AddSubview(pvController.View);
            pvController.DidMoveToParentViewController(this);

            View.AddSubview(pagerSurload);

            var connectButton = new UIButton
            {
                Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height - buttonHeight, UIScreen.MainScreen.Bounds.Width , buttonHeight),
                BackgroundColor = ApplicationColors.MainColor
            };
            connectButton.SetTitle( Core.Resources.StringValues.Startup_ConnectionButton_Text, UIControlState.Normal);
            View.AddSubview(connectButton);
            connectButton.SetCommand(ViewModel.GoToNextPageCommand);

		}

        public override void ViewWillAppear(bool animated)
        {
            NavigationController.SetNavigationBarHidden(true, false);
            base.ViewWillAppear(animated);
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			ScreenHelper.ClearAllHistory(NavigationController, this);
		}
	}

    public class PageDataSource : UIPageViewControllerDataSource
    {
        List<StartupPageFeatureViewController> _pages;
        private UIPageControl _pageControl;

        public PageDataSource(List<StartupPageFeatureViewController> pages, UIPageControl pageControl)
        {
            _pages = pages;
            _pageControl = pageControl;
        }

        override public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var currentPage = referenceViewController as StartupPageFeatureViewController;

            _pageControl.CurrentPage = currentPage.PageIndex;
            if (currentPage.PageIndex <= 0)
            {
                return null;
            }

            return _pages[currentPage.PageIndex - 1];
        }

        override public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
        {
            var currentPage = referenceViewController as StartupPageFeatureViewController;
            _pageControl.CurrentPage = currentPage.PageIndex;

            if (currentPage.PageIndex == _pages.Count - 1)
            {
                return null;
            }

            return _pages[(currentPage.PageIndex + 1)];
        }
    }
}

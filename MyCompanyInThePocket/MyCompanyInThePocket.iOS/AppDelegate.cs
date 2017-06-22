using System;
using System.Diagnostics;
using Foundation;
using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.iOS.Services;
using MyCompanyInThePocket.iOS.Views;
using UIKit;

namespace MyCompanyInThePocket.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }
       
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // On précise que la fenêtre prend toute la place de l’écran
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.BackgroundColor = ApplicationColors.WindowBackgroundColor;
            InitializeIoC();

            // Initialisation du contrôleur de vue par défaut 
            var viewController = new SplashScreenViewController();

            // Initialisation du contrôleur de navigation
            var navigationController = new UINavigationController(viewController);

            Window.RootViewController = navigationController;

            // Affiche la fenêtre principale
            Window.MakeKeyAndVisible();
			navigationController.SetNavigationBarHidden(true, false);
			navigationController.NavigationBar.TintColor = UIColor.White;
			navigationController.NavigationBar.BarTintColor = ApplicationColors.MainColor;
			navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
            Window.MakeKeyAndVisible();
            return true;
        }

        private void InitializeIoC()
        {
            if (!App.Instance.IsInitialized)
            {
                App.Instance.Initialize(
                    new iOSNavigationService(),
                    new IOSSqliteConnectionFactory(),
                    new IOSAuthentificationPlatformFactory(),
                    new iOSNativeCalendarIntegrationService(),
                    new iOSAlertService(),
                    new iOSBackgroundTaskService());
            }
        }

        public async override void PerformFetch(UIApplication application, System.Action<UIBackgroundFetchResult> completionHandler)
        {
			// Do Background Fetch
			var downloadSuccessful = false;
			try
			{

				InitializeIoC();
                var service = App.Instance.BackgroundTaskService;
                await service.RunInBackgroundAsync();
                downloadSuccessful = true;
			}
			catch (Exception ex)
			{
                Debug.WriteLine(ex.Message); 
			}

			if (downloadSuccessful)
			{
				completionHandler(UIBackgroundFetchResult.NewData);
			}
			else
			{
				completionHandler(UIBackgroundFetchResult.Failed);
			}
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }
    }
}



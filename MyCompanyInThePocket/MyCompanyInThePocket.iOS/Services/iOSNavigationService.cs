using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using System;
using MyCompanyInThePocket.iOS.Views;

namespace MyCompanyInThePocket.iOS.Services
{
    class iOSNavigationService : INavigationService
    {
        public void ShowViewMode<T>() where T : BaseViewModel
        {
            var type = typeof(T);

            switch (type.Name)
            {
                case nameof(MainViewModel):
                    CurrentViewController.Current.NavigationController.PushViewController(new MainViewController(), true);
                    break;

                case nameof(StartupViewModel):
                    CurrentViewController.Current.NavigationController.PushViewController(new StartupViewController(), true);
                    break;
                case nameof(SplashScreenViewModel):
                    CurrentViewController.Current.NavigationController.PushViewController(new SplashScreenViewController(),true);
                    break;

                default:
                    throw new NotImplementedException($"{type.Name} navigation is not implemented");
            }
        }
    }
}
using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using System;

namespace MyCompanyInThePocket.UWP.Services
{
    class DroidNavigationService : INavigationService
    {
        public void ShowViewMode<T>() where T : BaseViewModel
        {
            var type = typeof(T);
            
            switch (type.Name)
            {
                case nameof(MainViewModel):
                    //frame.Navigate(typeof(MainPage));
                    break;
                case nameof(StartupViewModel):
                    //frame.Navigate(typeof(StartupPage));
                    break;
                case nameof(SplashScreenViewModel):
                    //frame.Navigate(typeof(SplashScreenPage));
                    break;

                default:
                    throw new NotImplementedException($"{type.Name} navigation is not implemented");
            }
        }
    }
}
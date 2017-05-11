using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using MyCompanyInThePocket.UWP;
using MyCompanyInThePocket.UWP.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyCompanyInThePocket.UWP.Services
{
    class UWPNavigationService : INavigationService
    {
        public void ShowViewMode<T>() where T : BaseViewModel
        {
            var type = typeof(T);

            var frame = Window.Current.Content as Frame;
            switch (type.Name)
            {
                case nameof(MainViewModel):
                    frame.Navigate(typeof(MainPage));
                    break;
                case nameof(StartupViewModel):
                    frame.Navigate(typeof(StartupPage));
                    break;
                case nameof(SplashScreenViewModel):
                    frame.Navigate(typeof(SplashScreenPage));
                    break;

                default:
                    throw new NotImplementedException($"{type.Name} navigation is not implemented");
            }
        }
    }
}
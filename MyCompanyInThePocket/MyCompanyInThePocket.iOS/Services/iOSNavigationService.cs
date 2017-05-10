using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using System;

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
                    break;

                case nameof(StartupViewModel):
                    break;
                case nameof(SplashScreenViewModel):
                    break;

                default:
                    throw new NotImplementedException($"{type.Name} navigation is not implemented");
            }
        }
    }
}
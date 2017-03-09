using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Repositories.Interfaces;

namespace MyCompanyInThePocket.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
		private bool _useMock = false;

        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			RegisterAppStart<ViewModels.SplashScreenViewModel>();

            if (_useMock)
            {
                Mvx.RegisterType<IOnlineMeetingRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.MeetingRepository());
				Mvx.RegisterType<IOnlineUseFullLinkRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.UseFullLinkRepository());
            }
            else
            {
                Mvx.RegisterType<IOnlineMeetingRepository>(() => new MyCompanyInThePocket.Core.Repositories.OnlineRepositories.OnlineMeetingRepository());
				Mvx.RegisterType<IOnlineUseFullLinkRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.UseFullLinkRepository());
			}
			Mvx.RegisterType<IDbMeetingRepository>(() => new MyCompanyInThePocket.Core.Repositories.Database.DbMeetingRepository());
        }

    }
}

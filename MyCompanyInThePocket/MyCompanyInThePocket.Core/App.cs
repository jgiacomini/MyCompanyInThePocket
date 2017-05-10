using MyCompanyInThePocket.Core.Repositories.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Repositories.Database;

namespace MyCompanyInThePocket.Core
{
    public class App
    {
		private bool _useMock = false;

        private static readonly App _instance = new App();

        private App() { }

        public static App Instance
        {
            get
            {
                return _instance;
            }
        }
        
        public void Initialize(
            ISqliteConnectionFactory sqliteConnectionFactory,
            IAuthentificationPlatformFactory plaformFactory,
            INativeCalendarIntegrationService nativeCalendarIntegrationService,
            IMessageService messageService)
        {
            MessageService = messageService;

			NativeCalendarIntegrationService = nativeCalendarIntegrationService;
            
            SimpleIoc.Default.Register<IAuthentificationService>(() => new AuthentificationService(plaformFactory));
            if (_useMock)
            {
                SimpleIoc.Default.Register<IOnlineMeetingRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.MeetingRepository());
                SimpleIoc.Default.Register<IOnlineUseFullLinkRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.UseFullLinkRepository());
            }
            else
            {
                var authentificationService = SimpleIoc.Default.GetInstanceWithoutCaching<IAuthentificationService>();
                SimpleIoc.Default.Register<IOnlineMeetingRepository>(() => new MyCompanyInThePocket.Core.Repositories.OnlineRepositories.OnlineMeetingRepository(authentificationService));
                SimpleIoc.Default.Register<IOnlineUseFullLinkRepository>(() => new MyCompanyInThePocket.Core.Repositories.MockRepositories.UseFullLinkRepository());
			}


            SimpleIoc.Default.Register<IDatabaseService>(() => new DatabaseService(sqliteConnectionFactory));

            var databaseService = SimpleIoc.Default.GetInstanceWithoutCaching<IDatabaseService>();

            
            SimpleIoc.Default.Register<IDbMeetingRepository>(() => new DbMeetingRepository(sqliteConnectionFactory));

            var onlineMeetingRepository = SimpleIoc.Default.GetInstanceWithoutCaching<IOnlineMeetingRepository>();
            var dbMeetingRepository = SimpleIoc.Default.GetInstanceWithoutCaching<IDbMeetingRepository>();
            var onlineUseFullLinkRepository = SimpleIoc.Default.GetInstanceWithoutCaching<IOnlineUseFullLinkRepository>();

			         
            SimpleIoc.Default.Register<IMeetingService>(() => new MeetingsService(onlineMeetingRepository, dbMeetingRepository));
            SimpleIoc.Default.Register<IUseFullLinkService>(() => new UseFullLinkService(onlineUseFullLinkRepository));
        }

        public TService GetInstance<TService>()
        {
            return SimpleIoc.Default.GetInstanceWithoutCaching<TService>();
        }

        public IMessageService MessageService { get; private set; }

        public INativeCalendarIntegrationService NativeCalendarIntegrationService { get; private set; }

    }
}

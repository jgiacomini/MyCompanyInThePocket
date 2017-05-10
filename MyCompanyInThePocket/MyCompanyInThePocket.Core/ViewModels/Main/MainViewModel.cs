using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

		public MainViewModel(
			IAuthentificationService authentificationService,
			IMeetingService meetingService, 
			IUseFullLinkService useFullLinkService)
		{
			MeetingsVM = new MeetingsViewModel();
			SettingsVM = new SettingsViewModel(authentificationService);
			UseFullLinksVM = new UseFullLinksViewModel(useFullLinkService);
		}

		public MeetingsViewModel MeetingsVM
		{
			get;
			private set;
		}

		public SettingsViewModel SettingsVM
		{
			get;
			private set;
		}

		public UseFullLinksViewModel UseFullLinksVM
		{
			get;
			private set;
		}

    }
}

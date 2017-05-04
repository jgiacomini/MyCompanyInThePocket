using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.Services.Interface;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {

		public MainScreenViewModel(
			IAuthentificationService authentificationService,
			IMeetingService meetingService, 
			IUseFullLinkService useFullLinkService)
		{
			MeetingsVM = new MeetingsViewModel(meetingService);
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

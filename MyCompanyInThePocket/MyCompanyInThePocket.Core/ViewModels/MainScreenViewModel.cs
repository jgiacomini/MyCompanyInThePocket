using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Services.Interface;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class MainScreenViewModel : BaseViewModel
    {

		public MainScreenViewModel(IMeetingService meetingService, IUseFullLinkService useFullLinkService)
		{
			MeetingsVM = new MeetingsViewModel(meetingService);
			UseFullLinksVM = new UseFullLinksViewModel(useFullLinkService);
			SettingsVM = new SettingsViewModel();
		}

		public MeetingsViewModel MeetingsVM
		{
			get;
			private set;
		}

		public UseFullLinksViewModel UseFullLinksVM
		{
			get;
			private set;
		}

		public SettingsViewModel SettingsVM
		{
			get;
			private set;
		}
    }
}

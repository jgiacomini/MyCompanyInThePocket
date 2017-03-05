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

		public MainScreenViewModel(IMeetingService meetingService)
		{
			MeetingsVM = new MeetingsViewModel(meetingService);
			SettingsVM = new SettingsViewModel();
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
    }
}

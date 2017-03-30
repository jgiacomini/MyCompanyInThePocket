using System;
using System.Collections.Generic;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class GroupedMeetingViewModel : List<MeetingViewModel>
	{
		public GroupedMeetingViewModel(DateTime date, IEnumerable<MeetingViewModel> items)
			: base(items)
		{
			Date = date;
		}

		public DateTime Date
		{
			get;
			set;
		}
	}
}

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
			DateFormated = GetDateFormated();
			IsNow = Date.Date == DateTime.Now.Date;
		}

		public DateTime Date
		{
			get;
			private set;
		}

		public bool IsNow
		{
			get;
			private set;
		}

		private string GetDateFormated()
		{
			var result = string.Empty;
			if (DateTime.Now.Date == Date.Date)
			{
				result = $"Aujourd'hui • {Date.ToLongDateString()}";
			}
			else if (DateTime.Now.AddDays(1).Date == Date.Date)
			{
				result = $"Demain • {Date.ToLongDateString()}";
			}
			else
			{

				result = Date.ToLongDateString();
			}

			return result;
		}

		public string DateFormated
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return string.Format($"[GroupedMeetingViewModel: DateFormated={DateFormated}, IsNow={IsNow}]");
		}
	}
}

using System;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class MeetingViewModel
	{
		private readonly Meeting _meeting;
		private DateTime _date;
		private string _dateFormated;

		public MeetingViewModel(Meeting meeting, DateTime date)
		{
			_date = date;
			_meeting = meeting;
			_dateFormated = _date.ToShortDateString();
		}

		public DateTime Date
		{
			get
			{
				return _date;
			}
		}

		public string Title
		{
			get
			{
				return _meeting.Title;
			}
		}

		public string DateFormatted
		{
			get 
			{
				return _dateFormated;
			}
		}

		public string Type
		{
			get 
			{
				switch (_meeting.Type)
				{
					case MeetingType.AvantVente :
						return StringValues.MeetingType_AvantVente;

					case MeetingType.Conference:
					return StringValues.MeetingType_Conference;

					case MeetingType.CP_RTT:
					return StringValues.MeetingType_CP_RTT;

					case MeetingType.Mission:
					return StringValues.MeetingType_Mission;

					case MeetingType.NonFacture:
					return StringValues.MeetingType_NonFacture;
						
					default:
						return string.Empty;
				}
			}
		}

		public bool AllDayEvent
		{
			get 
			{
				return _meeting.AllDayEvent;
			}
		}
	}
}

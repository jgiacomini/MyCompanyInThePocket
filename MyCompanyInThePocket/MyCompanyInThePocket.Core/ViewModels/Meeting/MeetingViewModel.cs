using System;
using System.ComponentModel;
using System.Globalization;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class MeetingViewModel
	{
		private readonly Meeting _meeting;

		private string _dateFormated;

		public MeetingViewModel(Meeting meeting)
		{
			_meeting = meeting;
			InitializeDateFormated();
		}

		private void InitializeDateFormated()
		{
			// jeu. 02/03/16 - ven. 03/03/16
			if (_meeting.StartDate.Date != _meeting.EndDate.Date)
			{
				//TODO :localisation
				_dateFormated = $"{_meeting.StartDate.ToShortDateString()} - {_meeting.EndDate.ToShortDateString()}";
			}
			else
			{
				_dateFormated = _meeting.StartDate.ToShortDateString();
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

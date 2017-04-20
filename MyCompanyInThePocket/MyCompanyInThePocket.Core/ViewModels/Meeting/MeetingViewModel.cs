using System;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Resources;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class MeetingViewModel
	{
		private readonly Meeting _meeting;
		private DateTime _date;
		private string _durationFormated;

		public MeetingViewModel(Meeting meeting, DateTime date)
		{
			_date = date;
			_meeting = meeting;

			if (_meeting.AllDayEvent)
			{
				//TODO : localisation
				_durationFormated = "JOUR ENTIER";
			}
			else
			{
				//TODO : afficher heure de début + durée
				_durationFormated = _date.ToString("HH:mm:ss");  
			}
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

		public string DurationFormated
		{
			get 
			{
				return _durationFormated;
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

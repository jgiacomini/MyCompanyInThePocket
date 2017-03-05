using System;
using System.ComponentModel;
using System.Globalization;
using MyCompanyInThePocket.Core.Models;

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
			var culture = new CultureInfo("fr-FR");
			string[] names = culture.DateTimeFormat.AbbreviatedDayNames;

			// jeu. 02/03/16 - ven. 03/03/16
			if (_meeting.StartDate.Date != _meeting.EndDate.Date)
			{
				//TODO :localisation
				_dateFormated = $"{GetShortDate(_meeting.StartDate, names)} - {GetShortDate(_meeting.EndDate, names)}";
			}
			else
			{
				_dateFormated = GetShortDate(_meeting.StartDate, names);
			}
		}

		string GetShortDate(DateTimeOffset date, string[] names)
		{
			return $"{names[(int)date.DayOfWeek]} {date.Day:D2}/{date.Month:D2}/{date.Year}";
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
			get {
				return _meeting.Type;
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

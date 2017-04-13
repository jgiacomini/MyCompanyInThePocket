using System;
using System.Globalization;
using System.Linq;

namespace MyCompanyInThePocket.Core
{
	public static class DateTimeExtension
	{
		private static string[] _abbreviatedDayNames;

		private static string[] _dayNames;

		private static string[] _monthNames;

		static DateTimeExtension()
		{
			var culture = new CultureInfo("fr-FR");
			_abbreviatedDayNames = culture.DateTimeFormat.AbbreviatedDayNames;
			_dayNames = culture.DateTimeFormat.DayNames;
			_monthNames = culture.DateTimeFormat.MonthNames;

		}

		public static string ToShortDateString(this DateTime date)
		{
			var year = date.ToString("yy");
			return $"{_abbreviatedDayNames[(int)date.DayOfWeek]} {date.Day:D2}/{date.Month:D2}/{year}";
		}

		public static string ToLongDateString(this DateTime date)
		{
			return FirstCharToUpper($"{_dayNames[(int)date.DayOfWeek]} {date.Day:D2} {_monthNames[date.Month]}");
		}

		private static string FirstCharToUpper(string input)
		{
			if (String.IsNullOrEmpty(input))
				return input;
			return input[0].ToString().ToUpper() + input.Substring(1);
		}
	}
}

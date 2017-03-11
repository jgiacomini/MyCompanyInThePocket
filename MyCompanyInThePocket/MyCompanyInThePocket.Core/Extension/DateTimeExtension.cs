using System;
using System.Globalization;

namespace MyCompanyInThePocket.Core
{
	public static class DateTimeExtension
	{
		private static string[] _cultureNames;

		static DateTimeExtension()
		{
			var culture = new CultureInfo("fr-FR");
			_cultureNames = culture.DateTimeFormat.AbbreviatedDayNames;
		}

		public static string ToShortDateString(this DateTime date)
		{
			var year = date.ToString("yy");
			return $"{_cultureNames[(int)date.DayOfWeek]} {date.Day:D2}/{date.Month:D2}/{year}";
		}
	}
}

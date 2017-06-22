using System;
using System.Collections.Generic;
using MyCompanyInThePocket.Core.Models;

namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
	internal static class MeetingMapper
	{
		internal static List<Meeting> MapMeetings(this CalendarValue[] sharePointMeetings)
		{
			var meetings = new List<Meeting>(sharePointMeetings.Length);
			foreach (var meeting in sharePointMeetings)
			{
				var dbMeeting = new Meeting();
				dbMeeting.EndDate = meeting.EndDate;
				dbMeeting.StartDate = meeting.EventDate;
				dbMeeting.Title = meeting.Title;
                dbMeeting.IsRecurrent = meeting.fRecurrence;
				dbMeeting.Type = ConvertStringToMeetingType(meeting.TypeCRA);


				dbMeeting.AllDayEvent = meeting.fAllDayEvent.GetValueOrDefault();
				if (meeting.Title != null && meeting.Title.IndexOf("férié", StringComparison.OrdinalIgnoreCase) != -1)
				{
					dbMeeting.IsHoliday = true;
				}
				meetings.Add(dbMeeting);
			}

			return meetings;
		}

		private static MeetingType ConvertStringToMeetingType(string typeCRA)
		{ 
			switch (typeCRA)
			{
				case "1-Mission":
				return MeetingType.Mission;

				case "2-Avant-Vente":
				return MeetingType.AvantVente;
				
				case "3-CP/RTT":
				return MeetingType.CP_RTT;

				case "4-Conférence":
				return MeetingType.Conference;

				case "5-Non facturé":
					return MeetingType.Conference;
				default:
					return MeetingType.Unknown;
			}
		}
	}
}

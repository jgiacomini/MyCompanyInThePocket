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
				dbMeeting.Type = meeting.TypeCRA;
				dbMeeting.AllDayEvent = meeting.fAllDayEvent.GetValueOrDefault();
				meetings.Add(dbMeeting);
			}

			return meetings;
		}
	}
}

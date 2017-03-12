using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Repositories.MockRepositories
{
    internal class MeetingRepository : IOnlineMeetingRepository
    {

        public MeetingRepository()
        {
        }

        public async Task<List<Meeting>> GetMeetingAsync()
        {
            await Task.Delay(2000);

            var meetings = new List<Meeting>();

            for (int day = 0; day < 31; day++)
            {
                var currentDay = DateTime.Now.AddDays(day);

                if (currentDay.DayOfWeek == DayOfWeek.Friday)
                {
                    var meeting = new Meeting();
                    meeting.StartDate = currentDay;
                    meeting.EndDate = currentDay;
                    //TODO : random title
                    meeting.Title = $"Rendez vous (all day) jour :{day}";
                    meeting.AllDayEvent = false;
                }
                else if (currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    // On travaille jamais le samedi et dimanche :D
                    continue;
                }

                for (int hour = 10; hour < 18; hour++)
                {
                    var meeting = new Meeting();

                    meeting.StartDate = currentDay.AddHours(hour);
                    meeting.EndDate = meeting.StartDate.AddMinutes(20);
                    meeting.Title = $"RENDEZ VOUS jour :{day}, heure {hour}";
					meeting.Type = MeetingType.Mission;
                    meeting.AllDayEvent = false;
                    meetings.Add(meeting);
                }
            }

            return meetings;
        }
    }

    #region Generated object
    internal sealed class Rootobject
    {
        public CalendarValue[] value { get; set; }
    }

    internal sealed class CalendarValue
    {
        public string odatatype { get; set; }
        public string odataid { get; set; }
        public string odataetag { get; set; }
        public string odataeditLink { get; set; }
        public int FileSystemObjectType { get; set; }
        public int Id { get; set; }
        public string ContentTypeId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public bool? fAllDayEvent { get; set; }
        public bool fRecurrence { get; set; }
        public object ParticipantsPickerId { get; set; }
        public object Category { get; set; }
        public object FreeBusy { get; set; }
        public object Overbook { get; set; }
        public string TypeCRA { get; set; }
        public int? RefProjetId { get; set; }

        public int AuthorId { get; set; }
        public int EditorId { get; set; }
        public string OData__UIVersionString { get; set; }
        public bool Attachments { get; set; }
        public string GUID { get; set; }
    }
    #endregion
}

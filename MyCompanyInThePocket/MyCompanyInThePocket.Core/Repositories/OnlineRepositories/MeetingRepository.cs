using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
    internal class MeetingRepository : IOnlineRepository, IOnlineMeetingRepository
    {
        private AuthentificationService _authentificationService;

        public MeetingRepository(IAuthentificationPlatformFactory authentificationPlatformFactory)
        {
            _authentificationService = new AuthentificationService(authentificationPlatformFactory);
        }

        public async Task<List<Meeting>> GetMeetingAsync()
        {
            await _authentificationService.AuthenticateAsync();

            var client = _authentificationService.GetClient();

            var date = DateTime.Now.AddMonths(-2).ToString("s");
            var identity = OnlineSettings.Identity;

            var queryString = AuthentificationService.ServiceResourceId + $"ACRA/_api/web/lists/getbytitle('{identity}')/items?$top=1000&$filter=EventDate gt DateTime'{date}'";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, queryString);
            HttpResponseMessage response = await client.SendAsync(request);


            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // TODO : exception personalisée
                    _authentificationService.Disconnect();
                    // TODO : gérer l'exception et faire une exception personalisée
                    throw new InvalidOperationException();
                }
                else
                {
                    // TODO : gérer l'exception et faire une exception personalisée
                    throw new InvalidOperationException();
                }
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject(responseString, typeof(Rootobject)) as Rootobject;

                var sharePointMeetings = data.value.OrderBy(ap => ap.EventDate).ToArray();

                // TODO : exception personalisé lors du mapping
                var meetings = MapMeetings(sharePointMeetings);

                return meetings;
            }
        }

        private List<Meeting> MapMeetings(CalendarValue[] sharePointMeetings)
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

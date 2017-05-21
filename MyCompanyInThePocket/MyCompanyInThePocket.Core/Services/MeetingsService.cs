using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.ViewModels;
using System.Linq;

namespace MyCompanyInThePocket.Core.Services
{
    internal class MeetingsService : IMeetingService
    {
        private readonly IOnlineMeetingRepository _onlineMeetingReposity;
		private readonly IDbMeetingRepository _dbMeetingReposittory;


        public MeetingsService(IOnlineMeetingRepository onlineMeetingReposity, IDbMeetingRepository dbMeetingRepository)
        {
            _onlineMeetingReposity = onlineMeetingReposity;
			_dbMeetingReposittory = dbMeetingRepository;
        }

        private bool HasConnectivity
        {
            get
            {
                return Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
            }
        }

        public async Task<List<GroupedMeetingViewModel>> GetMeetingsAsync(bool forceRefresh, CancellationToken cancellationToken)
        {
            var onlineRepository = _onlineMeetingReposity;
			bool canRefresh = false;

			if (ApplicationSettings.LastACRARefresh.AddMinutes(5) < DateTime.Now)
			{
				canRefresh = true;
			}

			if (forceRefresh)
			{
				canRefresh = true;
			}

			// Si le téléphone n'a pas de connectivité on n'appelle même pas le serveur.
			if (!HasConnectivity)
			{
				canRefresh = false;
			}

			if (canRefresh)
            {
				try
				{
					List<Meeting> meetings = await onlineRepository.GetMeetingAsync();
					await _dbMeetingReposittory.UpsertAllMeetingsAsync(meetings, cancellationToken);
					ApplicationSettings.LastACRARefresh = DateTime.Now;
				}
				catch (TokenExpiredException ex)
				{
					throw ex;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					//ignore exception...
					// we will use DB meetigns
				}
            }

			var meetingsDB = await _dbMeetingReposittory.GetMeetingsSuperiorOfDateAsync(DateTime.Now.Date, cancellationToken);

            var groupedMeetings = ToGroupedMeetings(meetingsDB);
			var nativeCalendarIntegrationService = App.Instance.CalendarIntegrationService;

			if (nativeCalendarIntegrationService != null)
			{
				if (ApplicationSettings.IsIntegrationToCalendarEnabled)
				{
					await nativeCalendarIntegrationService.PushMeetingsToCalendarAsync(meetingsDB);
				}
				else
				{
					await nativeCalendarIntegrationService.DeleteCalendarAsync();
				}

                await AddACRAReminderAsync(nativeCalendarIntegrationService,groupedMeetings);
			}

            return groupedMeetings;
		}


        private List<GroupedMeetingViewModel> ToGroupedMeetings(List<Meeting> meetings)
        {
            var groupedMeetingsResult = new List<GroupedMeetingViewModel>();
            var flatMeetings = new List<MeetingViewModel>();
            foreach (var meeting in meetings)
            {
                var currentDate = meeting.StartDate;
                while (currentDate < meeting.EndDate)
                {
                    flatMeetings.Add(new MeetingViewModel(meeting, currentDate));
                    currentDate = currentDate.AddDays(1);
                }
            }

            var groupedMeetings = flatMeetings.GroupBy(m => m.Date).
                                  ToDictionary(m => m.Key, m => m.ToList());

            var finalDate = DateTime.Now.Date.AddMonths(4);
            var currentGroupedDate = DateTime.Now.Date;
            while (currentGroupedDate <= finalDate)
            {
                if (groupedMeetings.ContainsKey(currentGroupedDate))
                {
                    var currentGroup = groupedMeetings[currentGroupedDate];
                    groupedMeetingsResult.Add(new GroupedMeetingViewModel(currentGroupedDate, currentGroup.ToList()));
                }
                else
                {
                    if (currentGroupedDate.DayOfWeek != DayOfWeek.Sunday &&
                        currentGroupedDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        var noMeeting = new Meeting();
                        noMeeting.AllDayEvent = true;
                        noMeeting.EndDate = currentGroupedDate;
                        noMeeting.StartDate = currentGroupedDate;
                        // TODO : localisation
                        noMeeting.Title = "Aucun événement";
                        noMeeting.Type = MeetingType.Unknown;

                        var noMeetings = new List<MeetingViewModel>();
                        noMeetings.Add(new MeetingViewModel(noMeeting, currentGroupedDate));

                        groupedMeetingsResult.Add(new GroupedMeetingViewModel(currentGroupedDate, noMeetings));
                    }
                }

                currentGroupedDate = currentGroupedDate.AddDays(1);
            }

            return groupedMeetingsResult;
        }

        private async Task AddACRAReminderAsync(ICalendarIntegrationService nativeCalendarIntegrationService, List<GroupedMeetingViewModel> groupedMeetings)
		{

			// On ne fait rien de plus car le service n'est pas instancié
			if (nativeCalendarIntegrationService == null)
			{
				return;
			}

			if (ApplicationSettings.IsIntegrationToReminderEnabled)
			{

				var groupedOrderedMeetings = groupedMeetings.
													 Where(gm => gm.Date.Month == DateTime.Today.Month).
													 OrderByDescending(gm => gm.Date).
													 ToList();

				DateTime? dateToRemind = null;

				foreach (var meetingsByDay in groupedOrderedMeetings)
				{
					var day = meetingsByDay.Date.DayOfWeek;
					if (day == DayOfWeek.Sunday || day == DayOfWeek.Saturday)
						continue;

					if (!meetingsByDay.Any(m =>
									   m.MeetingSource.IsHoliday ||
									   m.MeetingSource.Type == MeetingType.CP_RTT))
					{
						dateToRemind = meetingsByDay.Date.
													Date.
													AddHours(12).
													AddMinutes(30);
						break;
					}
				}


				if (dateToRemind.HasValue)
				{
					await nativeCalendarIntegrationService.
													AddReminder("ACRA - ENVOYER LA PROD",
																"Envoi la PROD, c'est mieux pour tout le monde",
																dateToRemind.Value.ToUniversalTime());
				}
				else
				{
					//TODO : delete reminder
				}
			}
			else
			{
				//TODO : delete reminder
			}
		}

		public DateTime GetLastUpdateTime()
		{
			return ApplicationSettings.LastACRARefresh;
		}
	}
}

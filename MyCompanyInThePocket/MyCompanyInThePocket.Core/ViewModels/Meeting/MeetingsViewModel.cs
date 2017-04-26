using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Threading;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using MyCompanyInThePocket.Core.Models;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class MeetingsViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMeetingService _meetingService;
        private DateTime _lastUpdate;
        #endregion

        public MeetingsViewModel(IMeetingService meetingService)
        {
            Meetings = new SuspendableObservableCollection<GroupedMeetingViewModel>();
            _meetingService = meetingService;
            RefreshCommand = new MvxCommand(ForceRefresh);
            _lastUpdate = _meetingService.GetLastUpdateTime();
        }

        public MvxCommand RefreshCommand
        {
            get;
            private set;
        }

        public SuspendableObservableCollection<GroupedMeetingViewModel> Meetings
        {
            get;
            private set;
        }

        public Task InitializeAsync()
        {
            return RefreshMeetings(false);
        }

        void ForceRefresh()
        {
            RefreshMeetings(false);
        }

        public string LastUpdate
        {
            get
            {
                if (_lastUpdate == DateTime.MinValue)
                {
                    return string.Empty;
                }
                // TODO : localisation
                return $"Dernière mise à jour {_lastUpdate.ToShortDateString()}";
            }
        }

        private async Task RefreshMeetings(bool forceRefresh)
        {
            IsBusy = true;
            try
            {
                Meetings.PauseNotifications();
                var meetings = await _meetingService.GetMeetingsAsync(forceRefresh, CancellationToken.None);

                var nativeCalendarIntegrationService = Mvx.Resolve<INativeCalendarIntegrationService>();
                if (nativeCalendarIntegrationService != null)
                {
                    if (ApplicationSettings.IsIntegrationToNativeCalendarEnabled)
                    {
                        await nativeCalendarIntegrationService.PushMeetingsToCalendarAsync(meetings);
                    }
                    else
                    {
                        await nativeCalendarIntegrationService.DeleteCalendarAsync();
                    }
                }

                Meetings.Clear();

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
                        Meetings.Add(new GroupedMeetingViewModel(currentGroupedDate, currentGroup.ToList()));
                    }
                    else
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

                        Meetings.Add(new GroupedMeetingViewModel(currentGroupedDate, noMeetings));
                    }

                    currentGroupedDate = currentGroupedDate.AddDays(1);
                }

                _lastUpdate = _meetingService.GetLastUpdateTime();

                RaisePropertyChanged(nameof(Meetings));
                RaisePropertyChanged(nameof(LastUpdate));
            }
            catch (System.Exception ex)
            {
                await Mvx.Resolve<IMessageService>()
                     .ShowErrorToastAsync(ex, "Erreur lors de la récupération des rendez-vous.");
            }
            finally
            {
                Meetings.ResumeNotifications();
                IsBusy = false;
            }
        }
    }
}

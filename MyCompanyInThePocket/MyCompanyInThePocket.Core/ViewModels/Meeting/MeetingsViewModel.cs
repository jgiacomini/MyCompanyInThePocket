using GalaSoft.MvvmLight.Command;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCompanyInThePocket.Core.ViewModels
{
    public class MeetingsViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMeetingService _meetingService;
        private DateTime _lastUpdate;
        private string _lastUpdateStr;
		private CancellationTokenSource _pageTokenSource;
        #endregion

        public MeetingsViewModel()
        {
            Meetings = new SuspendableObservableCollection<GroupedMeetingViewModel>();
            _meetingService = App.Instance.GetInstance<IMeetingService>();
            RefreshCommand = new RelayCommand(ForceRefresh);
            UpdateLastUpdateText(_meetingService.GetLastUpdateTime());
        }

        public ICommand RefreshCommand
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
			_pageTokenSource = new CancellationTokenSource();
			return RefreshMeetings(false, _pageTokenSource.Token);
        }

		public void CancelQueries()
		{
			if (_pageTokenSource != null)
			{
				_pageTokenSource.Cancel();
			}
		}

        void ForceRefresh()
        {
			_pageTokenSource = new CancellationTokenSource();
			RefreshMeetings(false, _pageTokenSource.Token);
        }

        public string LastUpdate
        {
            get
            {
                return _lastUpdateStr;
            }
            set
            {
                Set(ref _lastUpdateStr, value);
            }
        }

        void UpdateLastUpdateText(DateTime lastUpdate)
        {
            _lastUpdate = lastUpdate;
			if (_lastUpdate == DateTime.MinValue)
			{
                LastUpdate = string.Empty;
			}
			// TODO : localisation
            LastUpdate =  $"Dernière mise à jour {_lastUpdate.ToShortDateString()}";
        }

        private async Task RefreshMeetings(bool forceRefresh, CancellationToken token)
        {
            IsBusy = true;
			try
			{
				Meetings.PauseNotifications();
				var meetings = await _meetingService.GetMeetingsAsync(forceRefresh, token);

                var nativeCalendarIntegrationService = App.Instance.CalendarIntegrationService;

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

							Meetings.Add(new GroupedMeetingViewModel(currentGroupedDate, noMeetings));
						}
					}

					currentGroupedDate = currentGroupedDate.AddDays(1);
				}

				await AddACRAReminderAsync(nativeCalendarIntegrationService);

				var lastUpdate = _meetingService.GetLastUpdateTime();
				UpdateLastUpdateText(lastUpdate);
			}
			catch (TaskCanceledException ex)
			{
				Debug.WriteLine(ex.Message);
			}
			catch (TokenExpiredException ex)
			{
				Debug.WriteLine(ex.Message);
                this.ShowViewModel<StartupViewModel>();
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine(ex.Message);
                App.Instance.MessageService.ShowErrorToastAsync(ex, "Erreur lors de la récupération des rendez-vous.");
			}
            finally
            {
				_pageTokenSource = null;
                Meetings.ResumeNotifications();
                IsBusy = false;
            }
        }

		private async Task AddACRAReminderAsync(ICalendarIntegrationService nativeCalendarIntegrationService)
		{

			// On ne fait rien de plus car le service n'est pas instancié
			if (nativeCalendarIntegrationService == null)
			{
				return;
			}

			if (ApplicationSettings.IsIntegrationToNativeReminderEnabled)
			{

				var groupedOrderedMeetings = Meetings.
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
    }
}

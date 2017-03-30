using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Threading;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;

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

		public async Task InitializeAsync()
		{
			await RefreshMeetings(false);	
		}

		async void ForceRefresh()
		{
			await RefreshMeetings(false);
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

				foreach (var item in flatMeetings.GroupBy(m=>m.Date))
				{
					Meetings.Add(new GroupedMeetingViewModel(item.Key, item.ToList()));
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

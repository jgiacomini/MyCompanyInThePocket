using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using MvvmCross.Core.ViewModels;

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
			_meetingService = meetingService;
			RefreshCommand = new MvxCommand(ForceRefresh);
			_lastUpdate = _meetingService.GetLastUpdateTime();
		}

		public MvxCommand RefreshCommand
		{
			get;
			private set;
		}

		public ObservableCollection<MeetingViewModel> Meetings
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

				return $"Dernière mise à jour {_lastUpdate.ToShortDateString()}";
			}
		}

		private async Task RefreshMeetings(bool forceRefresh)
		{
			IsBusy = true;
			try
			{
				var meetings = await _meetingService.GetMeetingsAsync(forceRefresh, CancellationToken.None);
				Meetings = new ObservableCollection<MeetingViewModel>(meetings.Select(m => new MeetingViewModel(m)));
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
				IsBusy = false;
			}
		}
	}
}

using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class MeetingsViewModel : BaseViewModel
	{
		private readonly IMeetingService _meetingService;

		public MeetingsViewModel(IMeetingService meetingService)
		{
			_meetingService = meetingService;
		}

		public ObservableCollection<MeetingViewModel> Meetings
		{
			get;
			private set;
		}

		public async Task InitializeAsync()
		{
			IsBusy = true;
			try
			{
				var meetings = await _meetingService.GetMeetingsAsync(false, CancellationToken.None);
				Meetings = new ObservableCollection<MeetingViewModel>(meetings.Select(m => new MeetingViewModel(m)));
				RaisePropertyChanged(nameof(Meetings));
			}
			catch (System.Exception ex)
			{
				await Mvx.Resolve<IMessageService>()
					 .ShowErrorToastAsync(ex, "Erreur lors de la récupération des rendez-vous.");
				// TODO : log something
			}
			finally
			{
				IsBusy = false;
			}

		}
	}
}

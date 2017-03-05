using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Services.Interface;

namespace MyCompanyInThePocket.Core.ViewModels
{
	public class MeetingsViewModel : BaseViewModel
	{
		private readonly IMeetingService _meetingService;

		public MeetingsViewModel(IMeetingService meetingService)
		{
			_meetingService = meetingService;
		}

		public async Task InitializeAsync()
		{
			IsBusy = true;
			try
			{
				var meetings = await _meetingService.GetMeetingsAsync();

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

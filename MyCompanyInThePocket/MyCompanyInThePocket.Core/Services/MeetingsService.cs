using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using MyCompanyInThePocket.Core.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;

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

        public async Task<List<Meeting>> GetMeetingsAsync(bool forceRefresh, CancellationToken cancellationToken)
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
				List<Meeting> meetings = await onlineRepository.GetMeetingAsync();
				await _dbMeetingReposittory.UpsertAllMeetingsAsync(meetings, cancellationToken);
				ApplicationSettings.LastACRARefresh = DateTime.Now;
            }

			var meetingsDB = await _dbMeetingReposittory.GetMeetingsSuperiorOfDateAsync(DateTime.Now.Date, cancellationToken);
			return meetingsDB;
		}

		public DateTime GetLastUpdateTime()
		{
			return ApplicationSettings.LastACRARefresh;
		}
	}
}

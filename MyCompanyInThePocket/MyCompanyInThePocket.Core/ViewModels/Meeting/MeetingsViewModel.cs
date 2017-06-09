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
            var dateToUse = _lastUpdate.ToLocalTime();
			// TODO : localisation
            LastUpdate =  $"Dernière mise à jour {dateToUse.ToShortDateString()} à {dateToUse.ToShortTimeString()}";
        }

        private async Task RefreshMeetings(bool forceRefresh, CancellationToken token)
        {
            IsBusy = true;
			try
			{
				Meetings.PauseNotifications();
				var meetings = await _meetingService.GetMeetingsAsync(forceRefresh, token);

				Meetings.Clear();
                Meetings.AddRange(meetings);
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
                await App.Instance.AlertService.ShowExceptionMessageAsync(ex, "Erreur lors de la récupération des rendez-vous.");
			}
            finally
            {
				_pageTokenSource = null;
                Meetings.ResumeNotifications();
                IsBusy = false;
            }
        }

    }
}

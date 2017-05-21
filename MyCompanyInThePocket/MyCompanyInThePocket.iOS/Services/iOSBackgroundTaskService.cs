using System;
using System.Threading;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core;
using MyCompanyInThePocket.Core.Services;
using UIKit;

namespace MyCompanyInThePocket.iOS.Services
{
    public class iOSBackgroundTaskService : IBackgroundTaskService
    {
        
		public iOSBackgroundTaskService()
        {
        }

        public double MinimumInterval => UIApplication.BackgroundFetchIntervalMinimum / 60;

        public void Register(double intervalBetweenUpdate)
        {
            // Convert in seconds
            var interval = intervalBetweenUpdate * 60;

            if (interval < UIApplication.BackgroundFetchIntervalMinimum)
            {
                interval = UIApplication.BackgroundFetchIntervalMinimum;
            }

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(interval);
        }


        public Task RunInBackgroundAsync()
        {
            var meetingService = App.Instance.GetInstance<IMeetingService>();
            return meetingService.GetMeetingsAsync(false, CancellationToken.None);
        }

        public void UnRegister()
        {
            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalNever);
        }
    }
}

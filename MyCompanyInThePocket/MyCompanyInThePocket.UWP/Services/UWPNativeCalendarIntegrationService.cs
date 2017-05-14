using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.UWP.Services
{
    public class UWPNativeCalendarIntegrationService : ICalendarIntegrationService
    {
        public Task AddReminder(string title, string notes, DateTime date)
        {
            return Task.Delay(1000);
        }

        public Task DeleteCalendarAsync()
        {
            return Task.Delay(1000);
        }

        public Task PushMeetingsToCalendarAsync(List<Meeting> meetings)
        {
            return Task.Delay(1000);
        }
    }
}
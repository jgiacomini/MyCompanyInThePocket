using MyCompanyInThePocket.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public interface ICalendarIntegrationService
    {
        Task PushMeetingsToCalendarAsync(List<Meeting> meetings);

        Task DeleteCalendarAsync();

		Task AddReminder(string title, string notes, DateTime date);
    }
}

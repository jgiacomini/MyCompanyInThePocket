using MyCompanyInThePocket.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.ViewModels;

namespace MyCompanyInThePocket.Core.Services
{
    public interface IMeetingService
    {
        Task<List<GroupedMeetingViewModel>> GetMeetingsAsync(bool forceRefresh, CancellationToken cancellationToken);

		DateTime GetLastUpdateTime();
    }
}

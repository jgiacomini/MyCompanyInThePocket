using MyCompanyInThePocket.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public interface IMeetingService
    {
        Task<List<Meeting>> GetMeetingsAsync(bool forceRefresh, CancellationToken cancellationToken);

		DateTime GetLastUpdateTime();
    }
}

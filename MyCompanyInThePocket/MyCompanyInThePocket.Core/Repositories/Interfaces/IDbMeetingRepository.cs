using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Models;

namespace MyCompanyInThePocket.Core.Repositories.Interfaces
{
	public interface IDbMeetingRepository
	{
		Task UpsertAllMeetingsAsync(List<Meeting> meetings, CancellationToken cancellationToken);

		Task<List<Meeting>> GetMeetingsSuperiorOfDateAsync(DateTime dateTime, CancellationToken cancellationToken);
	}
}

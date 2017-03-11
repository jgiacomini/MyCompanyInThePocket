using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Platform;
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;

namespace MyCompanyInThePocket.Core.Repositories.Database
{
	internal class DbMeetingRepository : IDbMeetingRepository
	{
		private readonly ISqliteConnectionFactory _sqliteConnectionFactory;

		public DbMeetingRepository()
		{
            _sqliteConnectionFactory = Mvx.Resolve<ISqliteConnectionFactory>();
		}

		public async Task UpsertAllMeetingsAsync(List<Meeting> meetings, CancellationToken cancellationToken)
		{
			var connection = _sqliteConnectionFactory.GetConnection();

			// Supression de tout les anciens meetings.
			// Ajout des nouveaux meetings.
			await connection.RunInTransactionAsync((SQLite.Net.SQLiteConnection syncConnection) =>
			{
				syncConnection.DeleteAll<Meeting>();
				syncConnection.InsertAll(meetings);
			}, cancellationToken);
		}

		public async Task<List<Meeting>> GetMeetingsSuperiorOfDateAsync(DateTime dateTime, CancellationToken cancellationToken)
		{
			var connection = _sqliteConnectionFactory.GetConnection();

			return await connection.Table<Meeting>().Where(m=> m.EndDate >= dateTime).
			          				ToListAsync(cancellationToken);
		}
	}
}

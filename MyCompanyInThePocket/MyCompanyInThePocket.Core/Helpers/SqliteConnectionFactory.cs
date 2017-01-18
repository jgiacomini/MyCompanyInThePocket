using MyCompanyInThePocket.Core.Models;
using PCLStorage;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Helpers
{
    /// <summary>
    /// Base class which manage the creation of the connection
    /// </summary>
    /// <seealso cref="MyCompanyInThePocket.Core.Helpers.ISqliteConnectionFactory" />
    public abstract class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        #region constantes
        private const string _DB_NAME = "database.db";
        #endregion

        public Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            return Task.Run(() =>
            {
                var dbpath = Path.Combine(FileSystem.Current.LocalStorage.Path, _DB_NAME);
                var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(GetPlatform(), new SQLiteConnectionString(dbpath, storeDateTimeAsTicks: true)));
                return new SQLiteAsyncConnection(connectionFactory);
            });
        }

        protected abstract ISQLitePlatform GetPlatform();

    }
}

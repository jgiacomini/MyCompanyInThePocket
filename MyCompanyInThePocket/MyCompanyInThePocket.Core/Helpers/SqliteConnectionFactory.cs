using PCLStorage;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using System;
using System.IO;

namespace MyCompanyInThePocket.Core.Helpers
{
    /// <summary>
    /// Base class which manage the creation of the connection
    /// </summary>
    /// <seealso cref="MyCompanyInThePocket.Core.Helpers.ISqliteConnectionFactory" />
    public abstract class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        #region constantes        
        /// <summary>
        /// The database name
        /// </summary>
        private const string _DB_NAME = "database.db";
        #endregion

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>return the sqlite connection</returns>
        public SQLiteAsyncConnection GetConnection()
        {
            var dbpath = Path.Combine(FileSystem.Current.LocalStorage.Path, _DB_NAME);
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(GetPlatform(), new SQLiteConnectionString(dbpath, storeDateTimeAsTicks: true)));
            return new SQLiteAsyncConnection(connectionFactory);
        }

        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <returns>return the platform implematation of Sqlite</returns>
        protected abstract ISQLitePlatform GetPlatform();

    }
}

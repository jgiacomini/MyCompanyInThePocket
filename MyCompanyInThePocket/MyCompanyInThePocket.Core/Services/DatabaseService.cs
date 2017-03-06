using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Models;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    internal class DatabaseService : IDatabaseService
    {
        #region Constantes
        private const int _DB_VERSION = 1;
        #endregion

        #region Fields
        private readonly ISqliteConnectionFactory _sqliteConnectionFactory;
        private SQLiteAsyncConnection _connection;
        #endregion

        public DatabaseService(ISqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
        }

        /// <summary>
        /// Initializes the database asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeDbAsync()
        {
            int databaseVersion = 0;

            _connection = _sqliteConnectionFactory.GetConnection();

            int.TryParse(await GetPragmaVersionAsync(), out databaseVersion);

            if (databaseVersion != _DB_VERSION)
            {
                await CreateTablesAsync();
            }

            await UpdatePragmaVersion(_DB_VERSION);
        }

        /// <summary>
        /// Creates the table asynchronous.
        /// </summary>
        /// <returns>return a task</returns>
        private async Task CreateTablesAsync()
        {
            var tables = GetListOfTables();
            await _connection.CreateTablesAsync(tables.ToArray());
        }

        /// <summary>
        /// Gets the list of tables.
        /// </summary>
        /// <returns>return list of table types</returns>
        private Type[] GetListOfTables()
        {
            var list = new List<Type>();
            list.Add(typeof(Meeting));
            list.Add(typeof(UseFullDocument));
            list.Add(typeof(UseFullLink));
            list.Add(typeof(User));
            return list.ToArray();
        }

        #region PragmaVersion

        /// <summary>
        /// Gets the pragma version.
        /// </summary>
        /// <returns>
        /// Table version number
        /// </returns>
        private async Task<string> GetPragmaVersionAsync()
        {
            return await _connection.ExecuteScalarAsync<string>("PRAGMA user_version");
        }

        /// <summary>
        /// Updates the pragma version.
        /// </summary>
        /// <param name="updateVersion">The update version.</param>
        /// <returns></returns>
        private Task<int> UpdatePragmaVersion(int updateVersion)
        {
            return _connection.ExecuteAsync($"PRAGMA user_version = {updateVersion}");
        }
        #endregion

    }
}
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Models;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        #region Fields
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
            int databaseVersion = 1;

            _connection = await _sqliteConnectionFactory.GetConnectionAsync();

            int.TryParse(await GetPragmaVersionAsync(), out databaseVersion);

            if (databaseVersion != _DB_VERSION)
            {
                await CreateTableAsync();
            }
        }

        /// <summary>
        /// Creates the table asynchronous.
        /// </summary>
        /// <returns>return a task</returns>
        private async Task CreateTableAsync()
        {
            var tables = GetListOfTables();
            foreach (var table in tables)
            {
                await _connection.CreateTablesAsync(table);
            }
        }

        /// <summary>
        /// Gets the list of tables.
        /// </summary>
        /// <returns>return list of table types</returns>
        private List<Type> GetListOfTables()
        {
            var list = new List<Type>();
            list.Add(typeof(Meeting));
            list.Add(typeof(UseFullDocument));
            list.Add(typeof(UseFullLink));
            list.Add(typeof(User));
            return list;
        }

        /// <summary>
        /// Gets the pragma version.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns>
        /// Table version number
        /// </returns>
        private async Task<string> GetPragmaVersionAsync()
        {
            return await _connection.ExecuteScalarAsync<string>("PRAGMA user_version");
        }
    }
}
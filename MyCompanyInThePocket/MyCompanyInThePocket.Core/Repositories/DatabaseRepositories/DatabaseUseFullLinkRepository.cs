using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Helpers;

namespace MyCompanyInThePocket.Core.Repositories.DatabaseRepositories
{
    internal class DatabaseUseFullLinkRepository
    {
        private ISqliteConnectionFactory _sqliteConnectionFactory;

        public DatabaseUseFullLinkRepository(ISqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
        }

        public Task<List<UseFullLink>> GetUseFullLinksAsync()
        {
            return _sqliteConnectionFactory.GetConnection().QueryAsync<UseFullLink>("SELECT * FROM UseFullLink");
        }
    }
}

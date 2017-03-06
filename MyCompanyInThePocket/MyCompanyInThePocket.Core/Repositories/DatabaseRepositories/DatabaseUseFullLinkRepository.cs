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

        public async Task SaveUseFullLinksAsync(List<UseFullLink> useFullLinks)
        {
            var existingLinks = await GetUseFullLinksAsync();

            var updatedLinks = new List<UseFullLink>();
            var newLinks = new List<UseFullLink>();
            var deleteLinks = new List<UseFullLink>();

            var links = useFullLinks.Select(l => l.Link).Union(existingLinks.Select(l => l.Link));

            foreach (var link in links)
            {
                var existingLink = existingLinks.FirstOrDefault(l => l.Link == link);
                var newLink = newLinks.FirstOrDefault(l => l.Link == link);
                if (existingLink != null && newLink != null)
                {
                    existingLink.Name = newLink.Name;
                    updatedLinks.Add(existingLink);
                }
                else if (existingLinks == null)
                {
                    newLinks.Add(newLink);
                }
                else
                {
                    deleteLinks.Add(existingLink);
                }
            }

            var connection = _sqliteConnectionFactory.GetConnection();
            await connection.ExecuteAsync($"DELETE FROM UseFullLink WHERE Id IN ({string.Join(", ", deleteLinks.Select(l => l.Id))})");
            await connection.UpdateAllAsync(updatedLinks);
            await connection.InsertAllAsync(newLinks);
        }
    }
}

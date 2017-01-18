using SQLite.Net.Async;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Helpers
{
    public interface ISqliteConnectionFactory
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
    }
}

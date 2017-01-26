using MyCompanyInThePocket.Core.Helpers;


namespace MyCompanyInThePocket.Core.Repositories.DatabaseRepositories
{
    public class DatabaseMeetingRepository
    {
        private ISqliteConnectionFactory _sqliteConnectionFactory;

        public DatabaseMeetingRepository(ISqliteConnectionFactory sqliteConnectionFactory)
        {
            _sqliteConnectionFactory = sqliteConnectionFactory;
        }
    }
}

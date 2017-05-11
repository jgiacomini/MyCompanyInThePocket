using MyCompanyInThePocket.Core.Helpers;
using SQLite.Net.Interop;

namespace MyCompanyInThePocket.UWP.Helpers
{
    public class DroidSqliteConnectionFactory : SqliteConnectionFactory
    {
        protected override ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
        }
    }
}

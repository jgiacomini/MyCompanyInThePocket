using MyCompanyInThePocket.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Interop;

namespace MyCompanyInThePocket.UWP.Helpers
{
    public class UWPSqliteConnectionFactory : SqliteConnectionFactory
    {
        protected override ISQLitePlatform GetPlatform()
        {
            return new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
        }
    }
}

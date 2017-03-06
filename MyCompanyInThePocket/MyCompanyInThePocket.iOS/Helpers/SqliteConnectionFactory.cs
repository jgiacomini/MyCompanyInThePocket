using System;
using MyCompanyInThePocket.Core.Helpers;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;

namespace MyCompanyInThePocket.iOS
{
	public class IOSSqliteConnectionFactory : SqliteConnectionFactory
	{
		protected override ISQLitePlatform GetPlatform()
		{
			return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
		}
	}
}

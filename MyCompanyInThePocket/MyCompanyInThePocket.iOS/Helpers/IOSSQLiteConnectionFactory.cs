using System;
using MyCompanyInThePocket.Core.Helpers;
using SQLite.Net.Interop;

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

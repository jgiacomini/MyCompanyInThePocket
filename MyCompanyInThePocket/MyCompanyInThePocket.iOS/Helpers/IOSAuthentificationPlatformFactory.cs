using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;

namespace MyCompanyInThePocket.iOS
{
	public class IOSAuthentificationPlatformFactory : IAuthentificationPlatformFactory
	{
		public IPlatformParameters GetPlatformParameter()
		{
			return new PlatformParameters(CurrentViewController.Current, false);
		}
	}
}
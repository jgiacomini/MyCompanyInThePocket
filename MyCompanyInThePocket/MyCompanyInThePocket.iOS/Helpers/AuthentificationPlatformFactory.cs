using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;

namespace MyCompanyInThePocket.iOS
{
	public class AuthentificationPlatformFactory: IAuthentificationPlatformFactory
	{
		public IPlatformParameters GetPlatformParameter()
		{
			return new PlatformParameters(CurrentViewController.Current, false);
		}
	}
}

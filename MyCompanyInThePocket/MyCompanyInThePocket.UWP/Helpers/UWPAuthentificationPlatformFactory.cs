using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;

namespace MyCompanyInThePocket.UWP.Helpers
{
    public class UWPAuthentificationPlatformFactory : IAuthentificationPlatformFactory
    {
        public IPlatformParameters GetPlatformParameter()
        {
            return new PlatformParameters(PromptBehavior.Auto, false);
        }
    }
}

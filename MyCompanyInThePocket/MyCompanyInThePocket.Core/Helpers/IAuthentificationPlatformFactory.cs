using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MyCompanyInThePocket.Core.Helpers
{
    public interface IAuthentificationPlatformFactory
    {
        IPlatformParameters GetPlatformParameter();
    }
}

using MyCompanyInThePocket.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MyCompanyInThePocket.UWP.Helpers
{
    public class AuthentificationPlatformFactory : IAuthentificationPlatformFactory
    {
        public IPlatformParameters GetPlatformParameter()
        {
            return new PlatformParameters(PromptBehavior.Auto, false);
        }
    }
}

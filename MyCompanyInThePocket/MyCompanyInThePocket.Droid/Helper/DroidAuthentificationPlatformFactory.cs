using MyCompanyInThePocket.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Plugin.CurrentActivity;

namespace MyCompanyInThePocket.UWP.Helpers
{
    public class DroidAuthentificationPlatformFactory : IAuthentificationPlatformFactory
    {
        public IPlatformParameters GetPlatformParameter()
        {
            return new PlatformParameters(CrossCurrentActivity.Current.Activity, false);
        }
    }
}

using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Models;

namespace MyCompanyInThePocket.Core.Repositories.MockRepositories
{
    internal class UseFullLinkRepository : IOnlineUseFullLinkRepository
    {
        public async Task<IEnumerable<UseFullLink>> GetUseFullLinksAsync()
        {
            await Task.Delay(2000);

            var useFullLinks = new List<UseFullLink>
            {
                new UseFullLink { Link = "http://mycompany.sharepoint.com", Name = "Espace Projets" },
                new UseFullLink { Link = "http://outlook.office365.com/owa", Name = "Outlook (Web)" },
                new UseFullLink { Link = "http://portal.office.com", Name = "Portail Office 365" },
                new UseFullLink { Link = "http://mycompany.visualstudio.com", Name = "VSTS" },
                new UseFullLink { Link = "http://portal.azure.com", Name = "Azure" },
                new UseFullLink { Link = "http://mycompany.delve.com", Name = "Delve" }
            };

            return useFullLinks;
        }
    }
}

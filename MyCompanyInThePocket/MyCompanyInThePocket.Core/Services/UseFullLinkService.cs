using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCompanyInThePocket.Core.Models;
using Plugin.Connectivity;
using MyCompanyInThePocket.Core.Repositories.Interfaces;

namespace MyCompanyInThePocket.Core.Services
{
    public class UseFullLinkService : IUseFullLinkService
    {
        private readonly IOnlineUseFullLinkRepository _onlineUseFullLinkRepository;

        public UseFullLinkService(IOnlineUseFullLinkRepository onlineUseFullLinkRepository)
        {
            _onlineUseFullLinkRepository = onlineUseFullLinkRepository;
        }

        public async Task<IEnumerable<UseFullLink>> GetUseFullLinksAsync()
        {
            IEnumerable<UseFullLink> useFullLinks;
            if (IsConnected())
            {
                useFullLinks = await _onlineUseFullLinkRepository.GetUseFullLinksAsync();

                // TODO: sauvegarder les liens en base de données, et les images (seulement si le liens est nouveau)
            }
            else
            {
                // TODO: récupérer les liens depuis la base de données
                useFullLinks = new List<UseFullLink>();
            }

            return useFullLinks;
        }

        private bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}

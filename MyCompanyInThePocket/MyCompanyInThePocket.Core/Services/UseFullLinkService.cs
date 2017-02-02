using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using MyCompanyInThePocket.Core.Services.Interface;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Services
{
    public class UseFullLinkService : IUseFullLinkService
    {
        private readonly IOnlineUseFullLinkRepository _onlineUseFullLinkRepository;

        public UseFullLinkService(IOnlineUseFullLinkRepository onlineUseFullLinkRepository)
        {
            _onlineUseFullLinkRepository = onlineUseFullLinkRepository;
        }

        public async Task<List<UseFullLink>> GetUseFullLinksAsync()
        {
            List<UseFullLink> useFullLinks;
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

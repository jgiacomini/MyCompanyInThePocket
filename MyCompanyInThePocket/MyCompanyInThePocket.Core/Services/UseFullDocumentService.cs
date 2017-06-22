using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyCompanyInThePocket.Core.Services
{
    internal class UseFullDocumentService : IUseFullDocumentService
    {
        private readonly IOnlineUseFullDocumentRepository _onlineService;


        public UseFullDocumentService(IOnlineUseFullDocumentRepository onlineService)
        {
            _onlineService = onlineService;
        }

        public async Task<List<UseFullDocument>> GetDocumentsAsync()
        {
            List<UseFullDocument> docs;
            if (IsConnected())
            {
                docs = await _onlineService.GetDocumentsAsync();

                // TODO: sauvegarder les liens en base de données, et les images (seulement si le liens est nouveau)
            }
            else
            {
                // TODO: récupérer les liens depuis la base de données
                docs = new List<UseFullDocument>();
            }

            return docs;
        }

        private bool IsConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}

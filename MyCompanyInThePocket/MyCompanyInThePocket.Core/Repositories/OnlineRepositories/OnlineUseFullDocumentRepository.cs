using System;
using System.Collections.Generic;
using MyCompanyInThePocket.Core.Models;
using MyCompanyInThePocket.Core.Services;
using MyCompanyInThePocket.Core.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
    public class OnlineUseFullDocumentRepository : IOnlineUseFullDocumentRepository
    {
        private IAuthentificationService _authentificationService;

		public OnlineUseFullDocumentRepository(IAuthentificationService authentificationService)
		{
			_authentificationService = authentificationService;
		}

        public async System.Threading.Tasks.Task<List<UseFullDocument>> GetDocumentsAsync()
		{
			var date = DateTime.Now.AddMonths(-1).ToString("s");
			var identity = OnlineSettings.Identity;
            var data = await _authentificationService.GetAsync<UseFullDocumentMapper.RootObject>($"_api/Web/GetList('/Lists/Liens%20utiles')/Items");
            var documents = data.MapDocuments();

            foreach (var doc in documents)
            {
                try
                {
                    doc.Data = await _authentificationService.GetBytesAsync(doc.Url);

                }
                catch (Exception ex)
                {

                }


            }

            return documents;
		}
    }
}

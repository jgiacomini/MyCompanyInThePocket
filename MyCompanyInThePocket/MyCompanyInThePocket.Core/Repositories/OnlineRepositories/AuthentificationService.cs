using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
    internal class AuthentificationService
    {
		#region Fields
        private IAuthentificationPlatformFactory _plaformFactory;
		#endregion

		#region Properties
		public static string ServiceResourceId
		{ 
			get
			{
				return Config.ServiceResourceId;
			}
		}
		public string Authority { get; private set; }
        public Uri ReturnUri { get; private set; }
        public string ClientId { get; private set; }
		#endregion

		public AuthentificationService(IAuthentificationPlatformFactory plaformFactory)
        {
            Authority = Config.Authority;
            ClientId = Config.ClientId;
            ReturnUri = new Uri(Config.ReturnUri);
            _plaformFactory = plaformFactory;
        }

        public async Task AuthenticateAsync()
        {
			if (string.IsNullOrWhiteSpace(OnlineSettings.AccessToken))
			{
				var authResult = await GetAccessTokenAsync(ServiceResourceId, _plaformFactory.GetPlatformParameter());

				var email = authResult.UserInfo.DisplayableId;

				var identity = GetIdentity(email);
				OnlineSettings.Identity = identity;
				OnlineSettings.AccessToken = authResult.AccessToken;
				OnlineSettings.FamilyName = authResult.UserInfo.FamilyName;
			}
        }

        private string GetIdentity(string email)
        {
            // TODO : gérer les erreurde parsing... 
            return email.Split('@')[0].ToUpperInvariant();
        }

        public void Disconnect()
        {
			var authContext = new AuthenticationContext(Authority);
			authContext.TokenCache.Clear();
			OnlineSettings.Clear();
        }

        private async Task<AuthenticationResult> GetAccessTokenAsync(string serviceResourceId, IPlatformParameters param)
        {
            var authContext = new AuthenticationContext(Authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
            var authResult = await authContext.AcquireTokenAsync(serviceResourceId, ClientId, ReturnUri, param);
            return authResult;
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OnlineSettings.AccessToken);
            return client;
        }

    }
}

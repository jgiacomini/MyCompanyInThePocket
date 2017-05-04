using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace MyCompanyInThePocket.Core.Services
{
	internal class AuthentificationService : IAuthentificationService
    {
		#region Fields
        private IAuthentificationPlatformFactory _plaformFactory;
		private AuthenticationContext _authContext;
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
			_authContext = new AuthenticationContext(Authority);
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
			_authContext.TokenCache.Clear();
			OnlineSettings.Clear();
        }

        private async Task<AuthenticationResult> GetAccessTokenAsync(string serviceResourceId, IPlatformParameters param)
        {
            return await _authContext.AcquireTokenAsync(serviceResourceId, ClientId, ReturnUri, param);
        }

		private HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OnlineSettings.AccessToken);
            return client;
        }

		public async Task<T> GetAsync<T>(string route)
			where T : class
		{
			var client = GetClient();
			var queryString = $"{AuthentificationService.ServiceResourceId}{route}";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, queryString);
			HttpResponseMessage response = await client.SendAsync(request);


			if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
            		if (_authContext.TokenCache.ReadItems().Any())
            		{
						_authContext = new AuthenticationContext(Authority);
            		}

					//var content = await response.Content.ReadAsStringAsync();
                    //Disconnect();
                    throw new TokenExpiredException();
                }
                else
                {
                    // TODO : gérer l'exception et faire une exception personalisée
                    throw new InvalidOperationException();
                }
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject(responseString, typeof(T)) as T;
            }
		}

    }
}

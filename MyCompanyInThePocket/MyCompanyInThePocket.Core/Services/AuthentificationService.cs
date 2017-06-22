﻿using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MyCompanyInThePocket.Core.Helpers;
using MyCompanyInThePocket.Core.Services;
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

        public async Task AuthenticateAsync(bool silentAndForced = false)
        {
            if (string.IsNullOrWhiteSpace(OnlineSettings.AccessToken) || silentAndForced)
            {
                var authResult = await GetAccessTokenAsync(ServiceResourceId, _plaformFactory.GetPlatformParameter(), silentAndForced);

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

        private Task<AuthenticationResult> GetAccessTokenAsync(string serviceResourceId, IPlatformParameters param, bool silent = false)
        {
            if (silent)
            {
                return _authContext.AcquireTokenSilentAsync(serviceResourceId, ClientId);
            }

            return _authContext.AcquireTokenAsync(serviceResourceId, ClientId, ReturnUri, param);
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
                    try
                    {
                        var before = OnlineSettings.AccessToken;

                        if (!string.IsNullOrEmpty(before))
                        {
                            // try to reconnect silently
                            await AuthenticateAsync(silentAndForced: true);

                            // test de succès
                            if (before != OnlineSettings.AccessToken)
                            {
                                // replay
                                return await GetAsync<T>(route);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    //var content = await response.Content.ReadAsStringAsync();
                    Disconnect();
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

		public async Task<byte[]> GetBytesAsync(string url)
		{
			var client = GetClient();

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage response = await client.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				if (response.StatusCode == HttpStatusCode.Forbidden ||
					response.StatusCode == HttpStatusCode.Unauthorized)
				{	
					//var content = await response.Content.ReadAsStringAsync();
					Disconnect();
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
                var rep = await response.Content.ReadAsByteArrayAsync();
                return rep;
			}
		}

    }
}

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace MyCompanyInThePocket.Core
{
    internal static class OnlineSettings
    {
        private static ISettings AppSettings
        {
            get
            {
				return CrossSettings.Current;
            }
        }

        private const string AccessTokenKey = "ADAL" + nameof(AccessToken);
        private const string IdentityKey = "ADAL" + nameof(Identity);
        private const string FamilyNameKey = "ADAL" + nameof(FamilyName);

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(AccessTokenKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AccessTokenKey, value);
            }
        }

        public static string Identity
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdentityKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdentityKey, value);
            }
        }

        public static string FamilyName
        {
            get
            {
                return AppSettings.GetValueOrDefault(FamilyNameKey, string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FamilyNameKey, value);
            }
        }

		public static void Clear()
        {
			Identity = null;
            AccessToken = null;
            FamilyName = null;
        }
    }
}
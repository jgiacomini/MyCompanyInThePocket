using Plugin.Settings;
using Plugin.Settings.Abstractions;
namespace MyCompanyInThePocket.Core.Repositories.OnlineRepositories
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
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
            AccessToken = null;
            FamilyName = null;
        }
    }
}
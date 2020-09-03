using System;
using System.Web;
using System.Web.Mvc;
using OAuth2.Client;
using OAuth2.Models;
using TCAdmin.SDK.Objects;
using TCAdminOAuth.Configurations;
using TCAdminOAuth.Configurations.OAuths;

namespace TCAdminOAuth.Models
{
    public enum OAuthProvider
    {
        Whmcs,
        Discord,
        Google,
        Github,
        Facebook,
    }

    public static class OAuthProviderHelper
    {
        public static readonly string RedirectUrl =
            new Uri(new UrlHelper(HttpContext.Current.Request.RequestContext).Action("Callback", "OAuth", new { },
                "https") ?? "").ToString();

        public static OAuthProvider ParseToProviderEnum(this string provider)
        {
            return (OAuthProvider) Enum.Parse(typeof(OAuthProvider), provider, true);
        }

        public static OAuthBase ToBase(this OAuthProvider oAuthProvider)
        {
            switch (oAuthProvider)
            {
                case OAuthProvider.Google:
                    return new GoogleOAuth();
                case OAuthProvider.Whmcs:
                    return new WhmcsOAuth();
                case OAuthProvider.Discord:
                    return new DiscordOAuth();
                case OAuthProvider.Github:
                    return new GithubOAuth();
                case OAuthProvider.Facebook:
                    return new FacebookOAuth();
                default:
                    throw new ArgumentOutOfRangeException(nameof(oAuthProvider), oAuthProvider, null);
            }
        }

        public static OAuth2Client ToClient(this OAuthProvider oAuthProvider)
        {
            return oAuthProvider.ToBase().GetClient();
        }

        public static void SyncCurrentUser(this OAuthProvider oAuthProvider, UserInfo userInfo)
        {
            var user = TCAdmin.SDK.Session.GetCurrentUser();
            SyncUser(oAuthProvider, userInfo, user);
        }

        public static void SyncUser(this OAuthProvider oAuthProvider, UserInfo userInfo, User user)
        {
            user.CustomFields[$"OAUTH::{oAuthProvider}"] = userInfo.Id;
            user.Save();
        }

        public static void UnSyncCurrentUser(this OAuthProvider oAuthProvider)
        {
            var user = TCAdmin.SDK.Session.GetCurrentUser();
            UnSyncUser(oAuthProvider, user);
        }

        public static void UnSyncUser(this OAuthProvider oAuthProvider, User user)
        {
            user.AppData.RemoveValue($"OAUTH::{oAuthProvider}");
            user.Save();
        }

        public static bool CurrentUserIsSynced(this OAuthProvider oAuthProvider)
        {
            var user = TCAdmin.SDK.Session.GetCurrentUser();
            return UserIsSynced(oAuthProvider, user);
        }

        public static bool UserIsSynced(this OAuthProvider oAuthProvider, User user)
        {
            return user.AppData[$"OAUTH::{oAuthProvider}"] != null && user.AppData.HasValue($"OAUTH::{oAuthProvider}");
        }
    }
}
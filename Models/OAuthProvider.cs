using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alexr03.Common.TCAdmin.Objects;
using Alexr03.Common.TCAdmin.Permissions;
using OAuth2.Client;
using OAuth2.Models;
using TCAdmin.GameHosting.SDK.Objects;
using TCAdmin.GameHosting.SDK.TeamSpeak;
using TCAdmin.Interfaces.Database;
using TCAdmin.SDK.Mail;
using TCAdmin.SDK.Objects;
using TCAdminOAuth.Configurations;
using TCAdminOAuth.Configurations.OAuths;
using TeamSpeakService = TCAdmin.GameHosting.SDK.Objects.TeamSpeakService;

namespace TCAdminOAuth.Models
{
    public class OAuthProvider : DynamicTypeBase
    {
        public OAuthProvider() : base("tcmodule_oauth_providers")
        {
        }

        public OAuthProvider(int id) : this()
        {
            this.SetValue("id", id);
            this.ValidateKeys();
            if (!this.Find())
            {
                throw new KeyNotFoundException("Cannot find OAuth Provider with ID: " + id);
            }
        }

        public static OAuthProvider GetByName(string name)
        {
            return GetAll().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public static List<OAuthProvider> GetAll()
        {
            return new OAuthProvider().GetObjectList(new WhereList()).Cast<OAuthProvider>().ToList();
        }

        public string Name => this.GetStringValue("name");
    }

    public static class OAuthProviderHelper
    {
        public static string RedirectUrl
        {
            get
            {
                using (var securityBypass = new SecurityBypass(true))
                {
                    return new Uri($"{new CompanyInfo().ControlPanelUrl}/OAuth/Callback").ToString();
                }
            }
        }

        public static OAuthBase ToBase(this OAuthProvider oAuthProvider)
        {
            var oAuthBase = oAuthProvider.Create<OAuthBase>();
            return oAuthBase;
        }

        public static OAuth2Client ToClient(this OAuthProvider oAuthProvider)
        {
            using (var securityBypass = new SecurityBypass(true))
            {
                var oAuthBase = oAuthProvider.ToBase();
                return oAuthBase.GetClient();
            }
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
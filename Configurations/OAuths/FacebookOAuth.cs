﻿using Alexr03.Common.TCAdmin.Objects;
using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Infrastructure;
using TCAdminOAuth.Models;

namespace TCAdminOAuth.Configurations.OAuths
{
    public class FacebookOAuth : OAuthBase
    {
        public override OAuth2Client GetClient()
        {
            var config = OAuthProvider.GetByName("Facebook").Configuration.Parse<OAuthProviderConfiguration>();
            return new FacebookClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = config.ClientId.Trim(),
                ClientSecret = config.ClientSecret.Trim(),
                RedirectUri = OAuthProviderHelper.RedirectUrl,
                Scope = "email"
            });
        }
    }
}
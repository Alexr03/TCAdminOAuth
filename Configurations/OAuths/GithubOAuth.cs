﻿using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Infrastructure;
using TCAdminOAuth.Models;

namespace TCAdminOAuth.Configurations.OAuths
{
    public class GithubOAuth : OAuthBase
    {
        public override OAuth2Client GetClient()
        {
            var config = new OAuthProvider().FindByType(GetType()).Configuration.GetConfiguration<OAuthProviderConfiguration>();
            return new GitHubClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = config.ClientId.Trim(),
                ClientSecret = config.ClientSecret.Trim(),
                RedirectUri = OAuthProviderHelper.RedirectUrl,
                Scope = "user read:user"
            });
        }
    }
}
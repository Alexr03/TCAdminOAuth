using System;
using System.ComponentModel.DataAnnotations;
using TCAdminOAuth.Models;

namespace TCAdminOAuth.Configurations
{
    public class OAuthProviderConfiguration
    {
        private OAuthProvider _provider;
        
        [Display(Name = "Enable this OAuth Provider", Description = "Enabling this will allow users to login with this provider.")]
        public bool Enabled { get; set; }
        
        [Display(Name = "Client ID", Description = "The Client ID you get from the provider.")]
        public string ClientId { get; set; } = "";

        [Display(Name = "Client Secret", Description = "The Client Secret you get from the provider.")]
        public string ClientSecret { get; set; } = "";

        [Display(Name = "Base URL", Description = "Override the default Base URL for the OAuth provider.")]
        public string BaseUrl { get; set; } = "";

        public static OAuthProviderConfiguration GetConfiguration(OAuthProvider provider)
        {
            var credentialsConfiguration =
                ConfigurationHelper.GetConfiguration<OAuthProviderConfiguration>($"{provider.ToString()}.Creds");
            credentialsConfiguration._provider = provider;
            return credentialsConfiguration;
        }

        public void SetConfiguration(OAuthProviderConfiguration model)
        {
            ConfigurationHelper.SetConfiguration($"{_provider.ToString()}.Creds", model);
        }

        public void Save()
        {
            SetConfiguration(this);
        }

        public void UpdateWith(OAuthProviderConfiguration model)
        {
            Enabled = model.Enabled;
            ClientId = model.ClientId;
            ClientSecret = model.ClientSecret;
        }
    }
}
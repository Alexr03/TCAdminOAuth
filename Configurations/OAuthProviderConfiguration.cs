﻿using System.ComponentModel.DataAnnotations;

namespace TCAdminOAuth.Configurations
{
    public class OAuthProviderConfiguration
    {
        [Display(Name = "Enable this OAuth Provider", Description = "Enabling this will allow users to login with this provider.")]
        public bool Enabled { get; set; }
        
        [Display(Name = "Client ID", Description = "The Client ID you get from the provider.")]
        public string ClientId { get; set; } = "";

        [Display(Name = "Client Secret", Description = "The Client Secret you get from the provider.")]
        public string ClientSecret { get; set; } = "";
        
        [Display(Name = "Allow Email Authentication",
            Description =
                "Enabling this will allow users to login to a TCAdmin account that matches their OAuth Email without linking it. This is potentially dangerous.")]
        public bool AllowEmailAuth { get; set; } = false;
    }
}
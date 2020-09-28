using System.ComponentModel.DataAnnotations;

namespace TCAdminOAuth.Configurations
{
    public class WhmcsProviderConfiguration : OAuthProviderConfiguration
    {
        [Display(Name = "Base URL", Description = "Override the default Base URL for the OAuth provider.")]
        public string BaseUrl { get; set; } = "";
    }
}
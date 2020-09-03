using System.ComponentModel.DataAnnotations;

namespace TCAdminOAuth.Configurations
{
    public class GlobalOAuthSettings
    {
        [Display(Name = "Allow Email Authentication",
            Description =
                "Enabling this will allow users to login to a TCAdmin account that matches their OAuth Email without linking it. This is potentially dangerous.")]
        public bool EnableEmailAuth { get; set; }

        public static GlobalOAuthSettings GetConfiguration()
        {
            var credentialsConfiguration =
                ConfigurationHelper.GetConfiguration<GlobalOAuthSettings>($"OAuth.Global.Settings");
            return credentialsConfiguration;
        }

        public void SetConfiguration(GlobalOAuthSettings model)
        {
            ConfigurationHelper.SetConfiguration($"OAuth.Global.Settings", model);
        }

        public void Save()
        {
            SetConfiguration(this);
        }

        public void UpdateWith(GlobalOAuthSettings model)
        {
            EnableEmailAuth = model.EnableEmailAuth;
        }
    }
}
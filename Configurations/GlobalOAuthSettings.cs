using System.ComponentModel.DataAnnotations;
using Alexr03.Common.TCAdmin.Configuration;

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
                new DatabaseConfiguration<GlobalOAuthSettings>(Globals.ModuleId, nameof(GlobalOAuthSettings))
                    .GetConfiguration();
            return credentialsConfiguration;
        }

        public void SetConfiguration(GlobalOAuthSettings model)
        {
            new DatabaseConfiguration<GlobalOAuthSettings>(Globals.ModuleId, nameof(GlobalOAuthSettings))
                .SetConfiguration(model);
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
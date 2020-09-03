using OAuth2.Client;

namespace TCAdminOAuth.Configurations
{
    public abstract class OAuthBase
    {
        public abstract OAuthProviderConfiguration GetConfiguration();

        public abstract OAuth2Client GetClient();
    }
}
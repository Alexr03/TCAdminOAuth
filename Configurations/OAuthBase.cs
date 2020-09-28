using OAuth2.Client;

namespace TCAdminOAuth.Configurations
{
    public abstract class OAuthBase
    {
        public OAuthBase()
        {
        }

        public abstract OAuth2Client GetClient();
    }
}
using System;
using Newtonsoft.Json.Linq;
using OAuth2.Client;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using RestSharp;
using TCAdminOAuth.Configurations;
using TCAdminOAuth.Models;

namespace TCAdminOAuth.Impl
{
    public class WhmcsClient : OAuth2Client
    {
        private readonly WhmcsProviderConfiguration _configuration = new OAuthProvider().FindByType(typeof(WhmcsClient))
            .Configuration.GetConfiguration<WhmcsProviderConfiguration>();

        public WhmcsClient(IRequestFactory factory, IClientConfiguration configuration) : base(factory, configuration)
        {
        }

        protected override UserInfo ParseUserInfo(string content)
        {
            var response = JObject.Parse(content);

            return new UserInfo
            {
                Id = response["sub"].Value<string>(),
                Email = response["email"].SafeGet(x => x.Value<string>()),
                FirstName = response["given_name"].Value<string>(),
                LastName = response["family_name"].Value<string>(),
            };
        }

        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            args.Request.AddParameter("access_token", AccessToken);
            args.Request.AddParameter("oauth_token", AccessToken);
            // Weird workaround for removing a existing param.
            args.Request.Parameters.RemoveAll(x => x.Name == "oauth_token");
        }

        public override string Name => "WHMCS";

        protected override Endpoint AccessTokenServiceEndpoint =>
            new Endpoint
            {
                BaseUri = _configuration.BaseUrl,
                Resource = "/oauth/token.php"
            };

        protected override Endpoint AccessCodeServiceEndpoint =>
            new Endpoint
            {
                BaseUri = _configuration.BaseUrl,
                Resource = "/oauth/authorize.php"
            };

        protected override Endpoint UserInfoServiceEndpoint =>
            new Endpoint
            {
                BaseUri = _configuration.BaseUrl,
                Resource = "/oauth/userinfo.php"
            };
    }
}
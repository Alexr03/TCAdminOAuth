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
        private readonly OAuthProviderConfiguration _configuration = OAuthProviderConfiguration.GetConfiguration(OAuthProvider.Whmcs);
        
        public WhmcsClient(IRequestFactory factory, IClientConfiguration configuration) : base(factory, configuration)
        {
        }

        protected override UserInfo ParseUserInfo(string content)
        {
            Console.WriteLine(content);
            var response = JObject.Parse(content);
            foreach (var keyValuePair in response)
            {
                Console.WriteLine($"{keyValuePair.Key} = {keyValuePair.Value}");
            }
            
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
            var fakeParam = new Parameter { Name = "oauth_token", Value = AccessToken };
            args.Request.AddParameter(fakeParam);
            args.Request.Parameters.Remove(fakeParam);
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
using System;
using System.Net;
using Newtonsoft.Json.Linq;
using OAuth2.Client;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using RestSharp;

namespace TCAdminOAuth.Impl
{
    public class DiscordClient : OAuth2Client
    {
        public DiscordClient(IRequestFactory factory, IClientConfiguration configuration) : base(factory, configuration)
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
                Id = response["id"].Value<string>(),
                Email = response["email"].SafeGet(x => x.Value<string>()),
                FirstName = "",
                LastName = "",
            };
        }

        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            args.Request.AddHeader("Authorization", $"Bearer {AccessToken}");
        }

        public override string Name => "DISCORD";

        protected override Endpoint AccessTokenServiceEndpoint =>
            new Endpoint
            {
                BaseUri = "https://discord.com",
                Resource = "/api/oauth2/token"
            };

        protected override Endpoint AccessCodeServiceEndpoint =>
            new Endpoint
            {
                BaseUri = "https://discord.com",
                Resource = "/api/oauth2/authorize"
            };

        protected override Endpoint UserInfoServiceEndpoint =>
            new Endpoint
            {
                BaseUri = "https://discord.com",
                Resource = "/api/users/@me"
            };
    }
}
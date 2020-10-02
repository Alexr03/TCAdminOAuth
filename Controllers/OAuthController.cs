﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Alexr03.Common.Web.Helpers;
using Newtonsoft.Json.Linq;
using OAuth2.Client;
using OAuth2.Models;
using TCAdmin.SDK.Objects;
using TCAdmin.SDK.Web.MVC;
using TCAdmin.SDK.Web.MVC.Controllers;
using TCAdminOAuth.Configurations;
using TCAdminOAuth.Models;

namespace TCAdminOAuth.Controllers
{
    public class OAuthController : BaseController
    {
        private static readonly Dictionary<Guid, OAuthRequestState> OAuthRequests =
            new Dictionary<Guid, OAuthRequestState>();

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View("Info");
            }

            var provider = new OAuthProvider(id.Value);
            var configurationJObject = provider.Configuration.Parse<JObject>();
            var o = configurationJObject.ToObject(provider.Configuration.Type);
            ViewData.TemplateInfo = new TemplateInfo
            {
                HtmlFieldPrefix = provider.Id.ToString(),
            };
            return View(provider.Configuration.View, o);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection model)
        {
            var provider = new OAuthProvider(id);
            Console.WriteLine("Id: " + id);
            var bindModel = model.Parse(ControllerContext, provider.Configuration.Type, provider.Id.ToString());
            provider.Configuration.SetConfiguration(bindModel);
            return Json(new
            {
                Message = provider.Name + " OAuth Settings successfully saved."
            });
        }

        [HttpGet]
        [ParentAction("AccountSecurity", "Index")]
        public ActionResult Unlink(OAuthProvider provider)
        {
            provider.UnSyncCurrentUser();
            return Redirect("/AccountSecurity?sso=true");
        }

        public async Task<ActionResult> Login(int id)
        {
            var provider = new OAuthProvider(id);
            var client = provider.ToClient();
            var guid = Guid.NewGuid();
            var oAuthRequestState = new OAuthRequestState
            {
                Provider = provider,
                RequestLoginState = TCAdmin.SDK.Session.IsAuthenticated()
                    ? OAuthRequestLoginState.Link
                    : OAuthRequestLoginState.Login,
                UserId = TCAdmin.SDK.Session.IsAuthenticated() ? TCAdmin.SDK.Session.GetCurrentUser().UserId : -1
            };
            OAuthRequests.Add(guid, oAuthRequestState);
            
            var redirectUri = new Uri(await client.GetLoginLinkUriAsync(guid.ToString()));
            return Redirect(redirectUri.ToString());
        }

        public async Task<ActionResult> Callback()
        {
            var code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                SetOAuthError("Callback code error. Disallowing OAuth Login.");
                return RedirectWithOAuthError();
            }

            var guidRequest = Guid.Parse(Request.QueryString["state"]);
            if (!OAuthRequests.ContainsKey(guidRequest))
            {
                SetOAuthError("Callback guid error. Disallowing OAuth Login.");
                return RedirectWithOAuthError();
            }

            var request = OAuthRequests[guidRequest];
            var provider = request.Provider;
            var client = provider.ToClient();
            try
            {
                var userInfo = await client.GetUserInfoAsync(new NameValueCollection {{"code", code}});
                switch (request.RequestLoginState)
                {
                    case OAuthRequestLoginState.Login:
                        return LoginUser(userInfo) ? Redirect("/") : RedirectWithOAuthError();
                    case OAuthRequestLoginState.Link:
                        provider.SyncUser(userInfo, new User(request.UserId));
                        return Redirect("/AccountSecurity?sso=true");
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (UnexpectedResponseException)
            {
                return RedirectWithOAuthError();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OAuthRequests.Remove(guidRequest);
            }

            return RedirectWithOAuthError();
        }

        private bool LoginUser(UserInfo userInfo)
        {
            var user = GetUserFromUserInfo(userInfo);
            if (user == null)
            {
                return false;
            }

            var cookie = FormsAuthentication.GetAuthCookie(user.UserId.ToString(), false);
            var cookieData = new FormsAuthenticationCookieData
            {
                ["lastlogdate"] = user.LastLoginUtc.ToString(CultureInfo.InvariantCulture),
                ["lastlogip"] = user.LastLoginIp,
                ["userkey"] = user.CustomFields["__TCA:COOKIE_KEY"].ToString(),
                ["userid"] = user.UserId.ToString()
            };

            var oldTicket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(oldTicket.Version, oldTicket.Name, oldTicket.IssueDate,
                oldTicket.Expiration.AddDays(10), oldTicket.IsPersistent, cookieData.ToString());
            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Response.Cookies.Add(cookie);

            return true;
        }

        private User GetUserFromUserInfo(UserInfo userInfo)
        {
            var provider = OAuthProvider.GetByName(userInfo.ProviderName);
            if (TCAdmin.SDK.Objects.User.GetAllUsers(2, true)
                .FindByCustomField($"OAUTH::{provider}", userInfo.Id) is User userByLink)
            {
                return userByLink;
            }

            if (provider.Configuration.Parse<OAuthProviderConfiguration>().AllowEmailAuth)
            {
                var users = TCAdmin.SDK.Objects.User.GetUsersByEmail(userInfo.Email).Cast<User>().ToList();
                switch (users.Count)
                {
                    case 0:
                        SetOAuthError("There are no accounts with this email. Disallowing OAuth Login.");
                        return null;
                    case 1:
                        return users[0];
                    default:
                        SetOAuthError("There is more than one user with this email address. Disallowing OAuth Login.");
                        break;
                }
            }
            else
            {
                SetOAuthError("Please link an account with this OAuth provider before logging in.");
            }

            return null;
        }

        private ActionResult RedirectWithOAuthError()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add("OAuthError", TempData["OAuthError"].ToString());

            return Redirect("/Login?" + queryString);
        }

        private void SetOAuthError(string error)
        {
            TempData["OAuthError"] = error;
        }
    }
}
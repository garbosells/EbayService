using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eBay;
using eBay.ApiClient.Auth.OAuth2;
using eBay.ApiClient.Auth.OAuth2.Model;

namespace EbayService.Controllers
{
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpGet]
        [Route("api/Authorization/GenerateUserAuthorizationUrl")]
        public Task<string> GenerateUserAuthorizationUrl()
        {
            OAuth2Api oAuth = new OAuth2Api();
            string url = oAuth.GenerateUserAuthorizationUrl(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production" ? OAuthEnvironment.SANDBOX : OAuthEnvironment.PRODUCTION, new string[] { "https://api.ebay.com/oauth/api_scope/sell.inventory" }, "test");
            return Task.FromResult<string>(url);
        }

        [HttpGet]
        [Route("api/Authorization/GetAuthTokenFromCode")]
        public Task<OAuthResponse> GetAuthTokenFromCode(string code)
        {
            OAuth2Api oAuth = new OAuth2Api();
            var response = oAuth.ExchangeCodeForAccessToken(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production" ? OAuthEnvironment.SANDBOX : OAuthEnvironment.PRODUCTION, code);
            return Task.FromResult<OAuthResponse>(response);
        }


    }
}

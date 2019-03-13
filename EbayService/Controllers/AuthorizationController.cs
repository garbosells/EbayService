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
    // GET api/GetLocations
    [HttpGet]
    [Route("api/Authorization/GenerateUserAuthorizationUrl")]
    public Task<string> GenerateUserAuthorizationUrl()
    {
      OAuth2Api oAuth = new OAuth2Api();
      string url = oAuth.GenerateUserAuthorizationUrl(OAuthEnvironment.SANDBOX, new string[] { "https://api.ebay.com/oauth/api_scope/sell.inventory" }, null);
      return Task.FromResult<string>(url);
    }
  }
}

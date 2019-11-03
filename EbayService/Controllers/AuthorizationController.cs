using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eBay;
using eBay.ApiClient.Auth.OAuth2;
using eBay.ApiClient.Auth.OAuth2.Model;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;
using EbayService.Managers.Interfaces;
using Microsoft.Extensions.Configuration;
using EbayService.Util;
using EbayService.Models;

namespace EbayService.Controllers
{
    [ApiController]
    public class AuthorizationController : Controller
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly IOptions<AppSettings> settings;
        private readonly IAuthorizationManager authorizationManager;
        private readonly IConfiguration configuration;

        public AuthorizationController(IOptions<AppSettings> settings, IAuthorizationManager authorizationManager, IConfiguration configuration)
        {
            this.settings = settings;
            this.authorizationManager = authorizationManager;
            this.configuration = configuration;
        }

        //TODO: Handle unsuccessful status code(s) in response
        /// <summary>
        /// Gets an Authorization URL to send to the user.
        /// The callback is set in the eBay key manager. It is associated with the redirecturi listed in ebay-config.yaml
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Authorization/GenerateUserAuthorizationUrl")]
        public async Task<GenerateUserAuthorizationUrlResponse> GenerateUserAuthorizationUrlAsync(long companyId)
        {
            try
            {
                OAuth2Api oAuth = new OAuth2Api();
                string url = oAuth.GenerateUserAuthorizationUrl(OAuthEnvironment.PRODUCTION, new string[] { "https://api.ebay.com/oauth/api_scope/sell.inventory" }, companyId.ToString());
                return await Task.FromResult<GenerateUserAuthorizationUrlResponse>(new GenerateUserAuthorizationUrlResponse { IsSuccess = true, URL = url });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return new GenerateUserAuthorizationUrlResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        //TODO: Handle unsuccessful status code(s) in response
        /// <summary>
        /// Endpoint called by eBay to return a code that can be exchanged for a user auth token.
        /// </summary>
        /// <param name="code">The code generated by eBay</param>
        /// <param name="key">A GUID string that must be included in eBay's callback</param>
        /// <param name="state">The companyId (ebay-defined parameter name)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Authorization/AuthSuccess")]
        public ActionResult GenerateAuthTokenFromCode(string code, long state)
        {
            OAuthResponse response = null;
            ViewResult result = new ViewResult();
            try
            {
                OAuth2Api oAuth = new OAuth2Api();
                response = oAuth.ExchangeCodeForAccessToken(OAuthEnvironment.PRODUCTION, code);
                authorizationManager.SetEbayAuth(response.AccessToken, response.RefreshToken, state);
                result.StatusCode = StatusCodes.Status200OK;
                result.ViewName = "AuthSuccess";
                return result;
            }
            catch (NotAuthorizedException ex)
            {
                telemetryClient.TrackException(ex, new Dictionary<string, string> { { "ErrorMessage", ex.Message } });
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex, new Dictionary<string, string> { { "ErrorMessage", response?.ErrorMessage } });
            }
            result.StatusCode = StatusCodes.Status500InternalServerError;
            result.ViewName = "AuthError";
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eBay.ApiClient.Auth.OAuth2.Model;
using EbayService.Managers.Interfaces;
using EbayService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;

namespace EbayService.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly IOptions<AppSettings> settings;

        public AuthorizationManager(IOptions<AppSettings> settings)
        {
            this.settings = settings;
        }

        public void SetEbayAuth(OAuthToken userToken, OAuthToken refreshToken )
        {
            var UserToken = new EbayOAuthToken {
                Token = userToken.Token,
                Expiration = userToken.ExpiresOn
            };
            var RefreshToken = new EbayOAuthToken
            {
                Token = refreshToken.Token,
                Expiration = refreshToken.ExpiresOn
            };
            var auth = new EbayAuth
            {
                UserToken = UserToken,
                RefreshToken = RefreshToken
            };
            
            
        }
    }
}

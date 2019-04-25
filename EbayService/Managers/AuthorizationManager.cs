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
        private readonly IKeyManager keyManager;

        public AuthorizationManager(IOptions<AppSettings> settings, IKeyManager keyManager)
        {
            this.settings = settings;
            this.keyManager = keyManager;
        }

        public void SetEbayAuth(OAuthToken userToken, OAuthToken refreshToken, long companyId)
        {
            var auth = new EbayAuth
            {
                UserToken = new EbayOAuthToken
                {
                    Token = userToken.Token,
                    Expiration = userToken.ExpiresOn,
                    Type = EbayOAuthTokenType.USERTOKEN
                },
                RefreshToken = new EbayOAuthToken
                {
                    Token = refreshToken.Token,
                    Expiration = refreshToken.ExpiresOn,
                    Type = EbayOAuthTokenType.REFRESHTOKEN
                }
            };

            keyManager.SetEbayAuthByCompanyId(companyId, auth);
        }
    }
}

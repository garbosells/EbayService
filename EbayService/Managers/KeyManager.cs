using EbayService.Managers.Interfaces;
using EbayService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayService.Util
{
    public class KeyManager : IKeyManager
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly KeyVaultClient keyVaultClient;
        private TelemetryClient telemetryClient = new TelemetryClient();
        private static string keyVaultUrl;

        public KeyManager(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
            keyVaultUrl = appSettings.Value.KeyVaultUrl;
            var tokenProvider = new AzureServiceTokenProvider(null, keyVaultUrl);
            keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    tokenProvider.KeyVaultTokenCallback));
        }

        public bool SetEbayAuthByCompanyId(long companyId, EbayAuth auth)
        {
            try
            {
                Task<bool> userTokenTask = SetEbayTokenByCompanyId(companyId, auth.UserToken);
                Task<bool> refreshTokenTask = SetEbayTokenByCompanyId(companyId, auth.RefreshToken);
                Task.WaitAll(userTokenTask, refreshTokenTask);
                if (!userTokenTask.Result || !refreshTokenTask.Result)
                    throw new Exception($"Error while setting ebay auth, user token result was {userTokenTask.ToString()} and refresh token result was {refreshTokenTask.ToString()}");
                else
                    return true;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return false;
            }
        }

        public async Task<EbayOAuthToken> GetEbayUserTokenByCompanyId(long companyId)
        {
            try
            {
                return await GetTokenSecret($"ebay-user-token-company{companyId}", EbayOAuthTokenType.USERTOKEN).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }

            return null;
        }

        public async Task<EbayOAuthToken> GetEbayRefreshTokenByCompanyId(long companyId)
        {
            try
            {
                return await GetTokenSecret($"ebay-refresh-token-company{companyId}", EbayOAuthTokenType.REFRESHTOKEN).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }

            return null;
        }

        public async Task<bool> SetEbayTokenByCompanyId(long companyId, EbayOAuthToken token)
        {
            try
            {
                var secretIdentifier = string.Empty;
                switch (token.Type)
                {
                    case EbayOAuthTokenType.USERTOKEN:
                        secretIdentifier = $"ebay-user-token-company{companyId}";
                        break;
                    case EbayOAuthTokenType.REFRESHTOKEN:
                        secretIdentifier = $"ebay-refresh-token-company{companyId}";
                        break;
                    default:
                        throw new InvalidTokenTypeException("The token has an invalid type.");
                }

                var secretAttributes = new SecretAttributes
                {
                    Expires = token.Expiration
                };

                //check to see if identifier exists
                var versionList = await keyVaultClient.GetSecretVersionsAsync(appSettings.Value.KeyVaultUrl, secretIdentifier);
                if(!versionList.Any())
                {
                    await keyVaultClient.SetSecretAsync(appSettings.Value.KeyVaultUrl, secretIdentifier, token.Token, null, null, secretAttributes);
                } else //exists, update it
                {
                    await keyVaultClient.UpdateSecretAsync(secretIdentifier, null, secretAttributes, null);
                }

                return true;
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
                return false;
            }
        }

        private async Task<EbayOAuthToken> GetTokenSecret(string identifier, EbayOAuthTokenType type)
        {

            try
            {
                var secretBundle = await keyVaultClient.GetSecretAsync(keyVaultUrl, identifier).ConfigureAwait(false);

                return new EbayOAuthToken
                {
                    Token = secretBundle.Value,
                    Expiration = secretBundle.Attributes.Expires,
                    Type = type
                };
            }
            catch (Exception ex)
            {
                telemetryClient.TrackException(ex);
            }
            return null;
        }
    }
}

using EbayService.Models;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.KeyVault;
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
    public class KeyManager
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

        public async Task<EbayOAuthToken> GetEbayUserTokenByCompanyId(long companyId)
        {
            try
            {
                var secretBundle = await keyVaultClient.GetSecretAsync(keyVaultUrl, $"ebay-user-token-company{companyId}").ConfigureAwait(false);

                return new EbayOAuthToken
                {
                    Token = secretBundle.Value,
                    Expiration = secretBundle.Attributes.Expires
                };
            } catch(Exception ex)
            {
                telemetryClient.TrackException(ex);
            }

            return null;
        }

        public async Task<EbayOAuthToken> GetEbayRefreshTokenByCompanyId(long companyId)
        {
            try
            {
                var secretBundle = await keyVaultClient.GetSecretAsync(keyVaultUrl, $"ebay-refresh-token-company{companyId}").ConfigureAwait(false);

                return new EbayOAuthToken
                {
                    Token = secretBundle.Value,
                    Expiration = secretBundle.Attributes.Expires
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

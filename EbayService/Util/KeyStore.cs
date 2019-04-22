using EbayService.Models;
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
    public class KeyStore
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly AzureServiceTokenProvider tokenProvider;
        private readonly KeyVaultClient keyVaultClient;
        private readonly DefaultKeyVaultSecretManager secretManager;

        public KeyStore(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
            var azureServiceTokenProvider = new AzureServiceTokenProvider(null, appSettings.Value.KeyVaultUrl);
            this.keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));
        }

        public async Task<EbayOAuthToken> GetEbayUserTokenByCompanyId(long companyId)
        {
            var secretBundle = await keyVaultClient.GetSecretAsync($"ebay-user-token-company{companyId}");
            return new EbayOAuthToken
            {
                Token = secretBundle.Value,
                Expiration = secretBundle.Attributes.Expires
            };
        }

        public async Task<EbayOAuthToken> GetEbayRefreshTokenByCompanyId(long companyId)
        {
            var secretBundle = await keyVaultClient.GetSecretAsync($"ebay-refresh-token-company{companyId}");
            return new EbayOAuthToken
            {
                Token = secretBundle.Value,
                Expiration = secretBundle.Attributes.Expires
            };
        }
    }
}

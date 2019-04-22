using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace EbayService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
            .ConfigureAppConfiguration((context, config) =>
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(
                        azureServiceTokenProvider.KeyVaultTokenCallback));

                config.AddAzureKeyVault(
                    $"https://ebay-key-vault.vault.azure.net/",
                    keyVaultClient,
                    new DefaultKeyVaultSecretManager());
            })
                .UseStartup<Startup>().Build();


    }
}

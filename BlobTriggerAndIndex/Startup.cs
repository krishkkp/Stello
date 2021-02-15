using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(BlobTriggerAndIndex.Startup))]
namespace BlobTriggerAndIndex
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configBuilder = new ConfigurationBuilder()
                                .AddEnvironmentVariables()
                                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

            // Building the KeyVault
            var config = configBuilder.Build();
            var keyVaultUri = "https://blobindexkeyvault.vault.azure.net/"; // config.GetValue<string>("VaultUri");
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            configBuilder.AddAzureKeyVault(keyVaultUri, keyVaultClient, new DefaultKeyVaultSecretManager());

            // Load the Search Settings
            config = configBuilder.Build();
            builder.Services.Configure<SearchConfigs>(config.GetSection(SearchConfigs.SearchSettings));
            builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), config));
            builder.Services.AddSingleton<IIndexManager, IndexerManager>();
        }
    }
}

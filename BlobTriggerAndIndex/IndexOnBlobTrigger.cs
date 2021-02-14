using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using Azure;
using Azure.Search.Documents.Indexes;

namespace BlobTriggerAndIndex
{
    public static class IndexOnBlobTrigger
    {
        const string adminAPIKey = @"C6F88345A8A5B60EF7B5BD44DC891801";
        const string endpoint = "https://hayanebula.search.windows.net";
        const string indexerName = "azureblob-indexer";

        [FunctionName("IndexOnBlobTrigger")]
        public static void Run([BlobTrigger("Documents/{name}", Connection = "VaultUri")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processing Started");

            SearchIndexerClient indexerClient = GetSearchIndexerClient();
            indexerClient.RunIndexer(indexerName);

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }


        public static SearchIndexerClient GetSearchIndexerClient()
        {
            return new SearchIndexerClient(new Uri(endpoint), new AzureKeyCredential(adminAPIKey));
        }

    }
}

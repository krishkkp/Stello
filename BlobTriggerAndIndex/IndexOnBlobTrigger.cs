using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using Azure;
using Azure.Search.Documents.Indexes;

namespace BlobTriggerAndIndex
{
    public class IndexOnBlobTrigger
    {
        private readonly IndexerManager indexerManager;

        public IndexOnBlobTrigger(IIndexManager indexManager)
        {
            this.indexerManager = (IndexerManager) indexManager;
        }


        [FunctionName("IndexOnBlobTrigger")]
        public void Run([BlobTrigger("Documents/{name}", Connection = "DefaultEndpointsProtocol=https;AccountName=hayablob;AccountKey=cbGtHHHzbIFnsdaNhQglkLSoaLWS7GRmVIFPjBaNuItf3TFwKv4WfO1kp0lDF/a9ZvvvmZD64AHSj93XnZndOw==;EndpointSuffix=core.windows.net")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processing Started");

            this.indexerManager.RunIndexer();
            
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}

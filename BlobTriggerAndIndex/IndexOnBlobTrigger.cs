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
        public void Run([BlobTrigger("hayablobcontainer/Documents/{name}")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processing Started");

            this.indexerManager.RunIndexer();
            
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}

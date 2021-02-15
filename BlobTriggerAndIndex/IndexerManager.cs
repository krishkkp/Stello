using Azure;
using Azure.Search.Documents.Indexes;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlobTriggerAndIndex
{
    class IndexerManager :  IIndexManager
    {
        public readonly SearchConfigs configs;

        public IndexerManager(IOptions<SearchConfigs> searchConfigs)
        {
            this.configs = searchConfigs?.Value;
        }

        public void RunIndexer()
        {
            SearchIndexerClient indexerClient = GetSearchIndexerClient();
            indexerClient.RunIndexer(configs.SearchIndexerName);
            return;
        }

        private SearchIndexerClient GetSearchIndexerClient()
        {
            return new SearchIndexerClient(
                new Uri(configs.SearchEndpointURL), 
                new AzureKeyCredential(configs.SearchAdminAPIKey));
        }
    }
}

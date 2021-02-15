using System;
using System.Collections.Generic;
using System.Text;

namespace BlobTriggerAndIndex
{
    public class SearchConfigs
    {
        public static string SearchSettings = nameof(SearchSettings);
        public string SearchEndpointURL { get; set; }
        public string SearchAdminAPIKey { get; set; }
        public string SearchIndexerName { get; set; }
        public string ConnectionString { get; set; }
    }
}

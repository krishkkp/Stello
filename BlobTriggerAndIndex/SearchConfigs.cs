using System;
using System.Collections.Generic;
using System.Text;

namespace BlobTriggerAndIndex
{
    public class SearchConfigs
    {
        public static string SearchSettings = nameof(SearchSettings);
        public string EndpointURI { get; set; }
        public string SearchAdminAPIKey { get; set; }
        public string IndexerName { get; set; }
        public string ConnectionString { get; set; }
    }
}

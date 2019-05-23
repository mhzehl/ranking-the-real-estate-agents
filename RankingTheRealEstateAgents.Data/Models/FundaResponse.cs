using System.Collections.Generic;
using Newtonsoft.Json;

namespace RankingTheRealEstateAgents.Data.Models
{
    public class FundaResponse
    {
        [JsonProperty("Objects")]
        public List<RealEstateObject> RealEstateObjects { get; set; }
        
        [JsonProperty("TotaalAantalObjecten")]
        public int RealEstateObjectCount { get; set; }
        
        [JsonProperty("Paging")]
        public PagingProperties PagingProperties { get; set; }
    }
}

using System;
using Newtonsoft.Json;

namespace RankingTheRealEstateAgents.Data.Models
{
    public class RealEstateObject
    {
        public Guid Id { get; set; }

        [JsonProperty("MakelaarId")]
        public int RealEstateAgentId { get; set; }

        [JsonProperty("MakelaarNaam")]
        public string RealEstateAgentName { get; set; }
    }
}

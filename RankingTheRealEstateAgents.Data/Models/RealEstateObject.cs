using System;
using Newtonsoft.Json;

namespace RankingTheRealEstateAgents.Data.Models
{
    public class RealEstateObject
    {
        public Guid Id { get; set; }

        [JsonProperty("makelaarId")]
        public int RealEstateAgentId { get; set; }

        [JsonProperty("makelaarNaam")]
        public string RealEstateAgentName { get; set; }
    }
}

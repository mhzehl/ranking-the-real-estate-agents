using System.Collections.Generic;

namespace RankingTheRealEstateAgents.Data.Models
{
    public class RealEstateAgent
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<RealEstateObject> RealEstateObjects { get; set; }
        
        public int ListingCount { get; set; }
    }
}

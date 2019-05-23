using System.Collections.Generic;
using System.Threading.Tasks;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public interface IRealEstateListingService
    {
        Task<List<RealEstateObject>> GetRealEstateObjects(string city = "amsterdam", string filter = "tuin");
    }
}

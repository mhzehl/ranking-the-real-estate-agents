using System.Collections.Generic;
using System.Threading.Tasks;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public class RealEstateListingService : IRealEstateListingService
    {
        private readonly IResilientFundaApiClient _client;

        public RealEstateListingService(IResilientFundaApiClient client)
        {
            _client = client;
        }

        public async Task<List<RealEstateObject>> GetRealEstateObjects(string city, string filter)
        {
            var listAllObjects = new List<RealEstateObject>();

            // Get results
            var result = await _client.QueryAsync(city, filter);

            while (result.PagingProperties.CurrentPage <= result.PagingProperties.PagesCount)
            {
                foreach (var listing in result.RealEstateObjects)
                {
                    var newListing = new RealEstateObject()
                    {
                        Id = listing.Id,
                        RealEstateAgentId = listing.RealEstateAgentId,
                        RealEstateAgentName = listing.RealEstateAgentName
                    };

                    listAllObjects.Add(newListing);
                }

                result = await _client.QueryAsync(city, filter, result.PagingProperties.CurrentPage+1);
            }

            return listAllObjects;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RankingTheRealEstateAgents.Core;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly IRealEstateListingService _realEstateListingService;

        public RankingController(IRealEstateListingService realEstateListingService)
        {
            _realEstateListingService = realEstateListingService;
        }

        public async Task<ActionResult> GetRealEstateObjects(string city = "amsterdam", string filter = "tuin")
        {
            var realEstateObjects = await _realEstateListingService.GetRealEstateObjects(city, filter);

            var topRealtors = GetTopRealtors(realEstateObjects);

            return Ok(topRealtors);
        }

        private static IEnumerable<RealEstateAgent> GetTopRealtors(List<RealEstateObject> realEstateObjects, int topNRealtors = 10)
        {
            // Group real estate objects by RealEstateAgentId, assuming this is a unique value. This also implies
            // I don't take into account real estate agents that belong to a group with multiple offices.
            // Order by the count of real estate objects per realtor in descending order.
            // Return the top 10 of realtors with the most real estate objects.

            var topRealtors = realEstateObjects
                .GroupBy(x => x.RealEstateAgentId)
                .Select(x => new RealEstateAgent()
                {
                    Id = x.Key,
                    ListingCount = x.Count(),
                    RealEstateObjects = x.ToList(),
                    Name = x.First().RealEstateAgentName
                })
                .ToList()
                .OrderByDescending(x => x.ListingCount)
                .Take(topNRealtors);

            return topRealtors;
        }
    }
}
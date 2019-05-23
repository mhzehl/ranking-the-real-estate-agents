using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public class FundaApiClient : IFundaApiClient
    {
        private readonly HttpClient _client;

        public FundaApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<FundaResponse> QueryAsync(string city, string filter, int page = 1)
        {
            string url = BuildUrl(city, filter, page);

            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Something went wrong. Please try again.");
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var streamContent = await new StreamReader(stream).ReadToEndAsync();

                return JsonConvert.DeserializeObject<FundaResponse>(streamContent);
            }
        }

        private static string BuildUrl(string city, string filter, int currentPage)
        {
            var buildUrl = $"?type=koop&zo=/";

            if (city != null)
            {
                buildUrl = $"{buildUrl}{city.ToLowerInvariant()}";
            }

            if (filter != null)
            {
                buildUrl = $"{buildUrl}/{filter}";
            }

            buildUrl = $"{buildUrl}/p{currentPage}";

            return buildUrl;
        }
    }
}

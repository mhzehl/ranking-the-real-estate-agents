using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RankingTheRealEstateAgents.Core.Policies;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public class ResilientFundaApiClient : IResilientFundaApiClient
    {
        private readonly HttpClient _client;
        private readonly ICustomPolicyWrap _customPolicyWrap;

        public ResilientFundaApiClient(HttpClient client, ICustomPolicyWrap customPolicyWrap)
        {
            _client = client;
            _customPolicyWrap = customPolicyWrap;
        }

        public async Task<FundaResponse> QueryAsync(string city, string filter, int page = 1)
        {
            string url = BuildUrl(city, filter, page);

            // Implement resiliency strategy to deal with faults while communicating
            // with the Funda API.
            var response = await _customPolicyWrap.DefineAndRetrieveResiliencyStrategy().ExecuteAsync(() => _client.GetAsync(url));

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
            var buildUrl = new StringBuilder("?type=koop&zo=/");

            if (city != null)
            {
                buildUrl.Append(city);
            }

            if (filter != null)
            {
                buildUrl.Append($"/{filter}");
            }

            buildUrl.Append($"/p{currentPage}");

            return buildUrl.ToString();
        }
    }
}

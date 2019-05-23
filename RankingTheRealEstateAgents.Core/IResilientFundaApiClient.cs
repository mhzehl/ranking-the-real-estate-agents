using System;
using System.Threading.Tasks;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public interface IResilientFundaApiClient
    {
        Task<FundaResponse> QueryAsync(string city, string filter, int page = 1);
    }
}

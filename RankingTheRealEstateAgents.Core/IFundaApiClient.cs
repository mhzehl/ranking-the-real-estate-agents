using System;
using System.Threading.Tasks;
using RankingTheRealEstateAgents.Data.Models;

namespace RankingTheRealEstateAgents.Core
{
    public interface IFundaApiClient
    {
        Task<FundaResponse> QueryAsync(string city, string filter, int page = 1);
    }
}

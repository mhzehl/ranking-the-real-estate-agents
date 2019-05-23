using Newtonsoft.Json;

namespace RankingTheRealEstateAgents.Data.Models
{
    public class PagingProperties
    {
        [JsonProperty("AantalPaginas")]
        public int PagesCount { get; set; }

        [JsonProperty("VolgendeUrl")]
        public string NextPageUrl { get; set; }

        [JsonProperty("HuidigePagina")]
        public int CurrentPage { get; set; }
    }
}

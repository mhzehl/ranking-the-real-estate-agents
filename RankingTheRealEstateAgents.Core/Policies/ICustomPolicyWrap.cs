using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Wrap;

namespace RankingTheRealEstateAgents.Core.Policies
{
    public interface ICustomPolicyWrap
    {
        AsyncPolicyWrap<HttpResponseMessage> DefineAndRetrieveResiliencyStrategy();
    }
}

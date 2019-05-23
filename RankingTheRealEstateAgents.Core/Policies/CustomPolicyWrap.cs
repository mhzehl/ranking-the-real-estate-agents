using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Polly;
using Polly.Wrap;

namespace RankingTheRealEstateAgents.Core.Policies
{
    public class CustomPolicyWrap : ICustomPolicyWrap
    {
        /// <summary>
        /// Creates a Polly-based resiliency strategy that helps deal with transient faults when communicating
        /// with the external Funda API service.
        /// </summary>
        /// <returns></returns>
        public AsyncPolicyWrap<HttpResponseMessage> DefineAndRetrieveResiliencyStrategy()
        {
            // Define our waitAndRetry policy: retry n times with an exponential back off in case the Funda API throttles us for too many requests.
            var waitAndRetryPolicy = Policy
                .HandleResult<HttpResponseMessage>(e => ShouldHandleWaitAndRetry(e))
                .WaitAndRetryAsync(10, // Retry 10 times with a delay between retries before ultimately giving up
                    attempt => TimeSpan.FromSeconds(0.25 * Math.Pow(2, attempt))
                );

            // Retry when these status codes are encountered.
            HttpStatusCode[] httpStatusCodesWorthRetrying = {
                HttpStatusCode.InternalServerError, // 500
                HttpStatusCode.BadGateway, // 502
                HttpStatusCode.GatewayTimeout // 504
            };

            // Define our first CircuitBreaker policy: Break if the action fails 4 times in a row.
            // This is designed to handle Exceptions from the Funda API, as well as
            // a number of recoverable status messages, such as 500, 502, and 504.
            var circuitBreakerPolicyForRecoverable = Policy
                .Handle<HttpResponseException>()
                .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromSeconds(3)
                );

            // Combine the waitAndRetryPolicy and circuit breaker policy into a PolicyWrap. This defines our resiliency strategy.
            return Policy.WrapAsync(waitAndRetryPolicy, circuitBreakerPolicyForRecoverable);
        }

        private static bool ShouldHandleWaitAndRetry(HttpResponseMessage e)
        {
            return e.StatusCode == HttpStatusCode.ServiceUnavailable ||
                   e.StatusCode == HttpStatusCode.TooManyRequests ||
                   (e.StatusCode == HttpStatusCode.Unauthorized && e.ReasonPhrase == "Request limit exceeded");
        }
    }
}

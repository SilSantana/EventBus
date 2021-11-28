using Demo.Api.Repository.Contracts.Booking;
using Demo.Api.Repository.Models.Booking;
using Microsoft.Extensions.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Refit;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Mock.Booking
{
    public class BookingMock
    {
        public static int CountRetry { get; private set; }

        public static IBookingProviderRepository GenerateProviderRepository()
        {
            Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError().OrResult(p => p.StatusCode == System.Net.HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt), (message, span,retryCount, context) => 
                {
                    CountRetry = retryCount;
                });

            Mock<PolicyHttpMessageHandler> policyHttpMessageHandler = new Mock<PolicyHttpMessageHandler>(retryPolicy);
            policyHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.Content.ReadAsStringAsync().Result.Contains("123")), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync((HttpRequestMessage m, CancellationToken ct) => SendAsync(m, ct, retryPolicy, new GetBookingResponse()));

            var httpClient = new HttpClient(policyHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://localhost/booking")
            };

            return RestService.For<IBookingProviderRepository>(httpClient);
        }


        protected static HttpResponseMessage SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken, AsyncRetryPolicy<HttpResponseMessage> retryPolicy, object value)
        {
            string output = JsonConvert.SerializeObject(value);
            return retryPolicy.ExecuteAsync((c, ct) => Task.FromResult<HttpResponseMessage>(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(output)
            }), new Context(), new CancellationToken()).Result;   
        }
    }
}

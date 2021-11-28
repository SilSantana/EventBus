using Refit;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Ioc.Handlers
{
    public class BookingApiHttpClientHandler : HttpClientHandler
    {
        private readonly RefitSettings _refitSettings;

        public BookingApiHttpClientHandler(RefitSettings refitSettings)
        {
            _refitSettings = refitSettings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("ApplicationID"))
            {
                request.Headers.Add("ApplicationID", "2021");
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

    }
}

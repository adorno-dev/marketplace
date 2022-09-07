using System.Net.Http.Headers;
using Hanssens.Net;

namespace Marketplace.Web.Handlers
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly LocalStorage localStorage;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TokenHandler(LocalStorage localStorage, IHttpContextAccessor httpContextAccessor)
        {
            this.localStorage = localStorage;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var isAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;

            localStorage.Load();

            if (isAuthenticated == true && localStorage.Exists("t"))
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", localStorage.Get<string>("t"));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
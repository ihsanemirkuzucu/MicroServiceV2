using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace MicroServiceV2.Order.Application.Contract.Refit
{
    public class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor):DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
                return await base.SendAsync(request, cancellationToken);

            //Authentice değilse token yok request devam etsin
            if(!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                return await base.SendAsync(request, cancellationToken);

            string? token = null;
            //tokenı almaya çalışılıyor
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                token = authHeader.ToString().Split(" ")[1];
            }

            //token null değilse headıra ekleniyor
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

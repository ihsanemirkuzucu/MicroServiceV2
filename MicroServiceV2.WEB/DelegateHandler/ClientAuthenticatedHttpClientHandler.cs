using Duende.IdentityModel.Client;
using MicroServiceV2.WEB.Options;
using MicroServiceV2.WEB.Services;

namespace MicroServiceV2.WEB.DelegateHandler
{
    public class ClientAuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor,
        IdentityOption identityOption,
        TokenService tokenService)
        :DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (httpContextAccessor.HttpContext is null)
                return await base.SendAsync(request, cancellationToken);

            //Authentice değilse token yok request devam etsin
            if (httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                return await base.SendAsync(request, cancellationToken);

            var tokenResponse  = await tokenService.GetClientAccessToken();
            if (tokenResponse.IsError)
                throw new UnauthorizedAccessException($"Client token request failed: {tokenResponse.Error}");

            request.SetBearerToken(tokenResponse.AccessToken!);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

using Duende.IdentityModel.Client;
using MicroServiceV2.Shared.Options;
using Microsoft.Extensions.DependencyInjection;

namespace MicroServiceV2.Order.Application.Contract.Refit
{
    public class ClientAuthenticatedHttpClientHandler(IServiceProvider serviceProvider,
        IHttpClientFactory httpClientFactory)
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(request.Headers.Authorization is not null)
                return await base.SendAsync(request, cancellationToken);

            using var scope = serviceProvider.CreateScope();
            var identityOptions = scope.ServiceProvider.GetRequiredService<IdentityOption>();
            var clientSecretOptions = scope.ServiceProvider.GetRequiredService<ClientSecretOptions>();

            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Address,
                Policy = { RequireHttps = false }
            };
            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(identityOptions.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest, cancellationToken: cancellationToken);
            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed {discoveryResponse.Error}");
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientSecretOptions.ClientId,
                ClientSecret = clientSecretOptions.ClientSecret,
            }, cancellationToken);
            if (tokenResponse.IsError)
            {
                throw new Exception($"Token request failed {discoveryResponse.Error}");
            }

            request.SetBearerToken(tokenResponse.AccessToken!);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

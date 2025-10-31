using Duende.IdentityModel.Client;
using MicroServiceV2.WEB.Options;
using MicroServiceV2.WEB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace MicroServiceV2.WEB.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption,
        HttpClient httpClient,
        TokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<SignInService> logger)
    {

        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {
            var tokenResponse = await GetAccessToken(signInViewModel);
            if (tokenResponse.IsError)
            {
                return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }
            var userClaims = tokenService.ExtractClaim(tokenResponse.AccessToken!);
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);
            var claimIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme,ClaimTypes.Name,ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal, authenticationProperties);


            return ServiceResult.Success();
        }

        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            httpClient.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(discoveryRequest);

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed {discoveryResponse.Error}");
            }

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password,
                Scope = "offline_access"
            });

            return tokenResponse;
        }

    }
}

using Duende.IdentityModel.Client;
using MicroServiceV2.WEB.Options;
using MicroServiceV2.WEB.Services;
using System.Net;

namespace MicroServiceV2.WEB.Pages.Auth.SignUp
{
    public class SignUpService(IdentityOption identityOption, HttpClient httpClient, ILogger<SignUpService> logger)
    {
        public async Task<ServiceResult> CreateAccount(SignUpViewModel signUpViewModel)
        {
            var token = await GetClientCredentialTokenAsAdmin();

            var address = $"{identityOption.BaseAddress}/admin/realms/MicroServiceTenant/users";
            httpClient.SetBearerToken(token);

            var userCreateRequest = CreateUserRequest(signUpViewModel);

            var response = await httpClient.PostAsJsonAsync(address, userCreateRequest);
            if (!response.IsSuccessStatusCode)
            {
                //Kullanıcı kaynaklı hataları yakalama
                if (response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var keycloakErrorResponse = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                    return ServiceResult.Error(keycloakErrorResponse.ErrorMessage);
                }
                //Server kaynaklı hata
                var error = await response.Content.ReadAsStringAsync();
                logger.LogError(error);
                return ServiceResult.Error("System error occurred. Please try again later.");
            }
            return ServiceResult.Success();
        }

        private async Task<string> GetClientCredentialTokenAsAdmin()
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

            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret,
            });
            if (tokenResponse.IsError)
            {
                throw new Exception($"Token request failed {discoveryResponse.Error}");
            }

            return tokenResponse.AccessToken!;
        }

        private static UserCreateRequest CreateUserRequest(SignUpViewModel signUpViewModel)
        {
            return new UserCreateRequest(
                signUpViewModel.UserName,
                true,
                signUpViewModel.FirstName,
                signUpViewModel.LastName,
                signUpViewModel.Email,
                [new Credential("password", signUpViewModel.Password, false)]);
        }
    }
}

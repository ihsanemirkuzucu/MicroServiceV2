namespace MicroServiceV2.WEB.Pages.Auth.SignUp;

public record UserCreateRequest(
    string Username,
    bool Enabled,
    string FirstName,
    string LastName,
    string Email,
    List<Credential> Credentials);
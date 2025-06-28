namespace Application.Users.Login;

public sealed class AuthToken
{
    public string AccessToken { get; init; } = default!;
    public string RefreshToken { get; init; } = default!;
}

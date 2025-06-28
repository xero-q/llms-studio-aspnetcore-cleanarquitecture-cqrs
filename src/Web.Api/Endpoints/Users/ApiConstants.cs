namespace Web.Api.Endpoints.Users;

public static class ApiConstants
{
    private const string Base = "api/auth";
    
    public const string Signup = $"{Base}/signup";
    public const string Login = $"{Base}/login";
    public const string Refresh = $"{Base}/refresh";
}

using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(int userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByUsername = Error.NotFound(
        "Users.NotFoundByUsername",
        "The user with the specified username was not found");
    
    public static readonly Error NotFoundByRefreshToken = Error.Failure(
        "Users.NotFoundByRefreshToken",
        "The refresh token provided was not found");
   
    public static readonly Error InvalidPassword = Error.Validation(
        "Users.InvalidPassword",
        "The password provided for this user is invalid");
    

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
}

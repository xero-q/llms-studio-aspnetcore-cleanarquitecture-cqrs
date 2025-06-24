using Application.Abstractions.Messaging;

namespace Application.Users.GetByUsername;

public sealed record GetUserByUsernameQuery(string Username) : IQuery<UserResponse>;

using Application.Abstractions.Messaging;
using Application.Users.Login;

namespace Application.Users.Refresh;

public sealed record RefreshUserCommand(string token) : ICommand<UserTokenResponse>;

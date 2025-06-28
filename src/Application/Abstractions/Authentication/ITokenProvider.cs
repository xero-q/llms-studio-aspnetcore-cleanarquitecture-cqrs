using Application.Users.Login;
using Domain.Users;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    AuthToken Create(User user);
}

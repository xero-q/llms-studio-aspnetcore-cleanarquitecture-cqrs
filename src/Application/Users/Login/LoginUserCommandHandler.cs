using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, UserTokenResponse>
{
    public async Task<Result<UserTokenResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == command.Username, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserTokenResponse>(UserErrors.NotFoundByUsername);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<UserTokenResponse>(UserErrors.InvalidPassword);
        }

        AuthToken token = tokenProvider.Create(user);
        
        //Save refresh token in the DB
        var refreshToken = new RefreshToken
        {
            Token = token.RefreshToken,
            UserId = user.Id
        };
        
        context.RefreshTokens.Add(refreshToken);

        await context.SaveChangesAsync(cancellationToken);

        return new UserTokenResponse
        {
            Access = token.AccessToken,
            Refresh = token.RefreshToken,
        };
    }
}

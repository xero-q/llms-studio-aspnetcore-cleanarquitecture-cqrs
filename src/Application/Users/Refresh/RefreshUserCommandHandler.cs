using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Users.Login;
using Domain.RefreshTokens;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Refresh;

internal sealed class RefreshUserCommandHandler(
    IApplicationDbContext context,
    ITokenProvider tokenProvider) : ICommandHandler<RefreshUserCommand, UserTokenResponse>
{
    public async Task<Result<UserTokenResponse>> Handle(RefreshUserCommand command, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == command.token, cancellationToken);

        if (refreshToken is null)
        {
            return Result.Failure<UserTokenResponse>(UserErrors.NotFoundByRefreshToken);
        }
        
        int userId = refreshToken.User.Id;

        AuthToken authToken = tokenProvider.Create(refreshToken.User);

        context.RefreshTokens.Remove(refreshToken);
        
        //Save refresh token in the DB
        var newRefreshToken = new RefreshToken
        {
            Token = authToken.RefreshToken,
            UserId = userId
        };
        
        context.RefreshTokens.Add(newRefreshToken);

        await context.SaveChangesAsync(cancellationToken);

        return new UserTokenResponse
        {
            Access = authToken.AccessToken,
            Refresh = authToken.RefreshToken,
        };
    }
}

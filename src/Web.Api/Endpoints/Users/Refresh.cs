using Application.Users.Login;
using Application.Users.Refresh;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Refresh : IEndpoint
{
    public sealed record Request(string refresh);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Refresh, async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new RefreshUserCommand(request.refresh);

                Result<UserTokenResponse> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Users);
    }
}

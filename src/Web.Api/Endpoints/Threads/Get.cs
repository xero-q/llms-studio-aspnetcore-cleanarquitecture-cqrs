using Application.Threads.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Threads;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Get, async (int page, int pageSize, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetThreadsQuery(page, pageSize);

            Result<PaginatedThreadResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Threads)
        .RequireAuthorization();
    }
}

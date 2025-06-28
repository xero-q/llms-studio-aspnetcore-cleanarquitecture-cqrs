using Application.Threads.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Threads;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiConstants.Delete, async (int threadId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteThreadCommand(threadId);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Threads)
        .RequireAuthorization();
    }
}

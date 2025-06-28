using Application.Threads.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Threads;

internal sealed class Create : IEndpoint
{
    public sealed class Request
    {
        public string Title { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Create, async (int modelId, Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateThreadCommand
            {
                Title = request.Title,
                ModelId = modelId
            };

            Result<int> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Threads)
        .RequireAuthorization();
    }
}

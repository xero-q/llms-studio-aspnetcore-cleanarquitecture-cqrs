using Application.Models.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Models;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.GetById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetModelByIdQuery(id);

            Result<ModelResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Models)
        .RequireAuthorization();
    }
}

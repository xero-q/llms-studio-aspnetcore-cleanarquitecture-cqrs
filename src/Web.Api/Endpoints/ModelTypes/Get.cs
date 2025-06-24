using Application.ModelTypes.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ModelTypes;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Get, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetModelTypesQuery();

            Result<List<ModelTypeResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.ModelTypes)
        .RequireAuthorization();
    }
}

using Application.Models.Get;
using Application.Prompts.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Prompts;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(ApiConstants.Get, async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GetPromptsQuery(id);

                Result<List<PromptResponse>> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Prompts)
            .RequireAuthorization();
    }
}

using Application.Prompts.Create;
using Application.Prompts.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Prompts;

internal sealed class Create:IEndpoint
{
    public sealed class Request
    {
        public string Prompt { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Create, async (int id, Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreatePromptCommand
                {
                    PromptText = request.Prompt,
                    ThreadId = id
                };

                Result<PromptResponse> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Prompts)
            .RequireAuthorization();
    }
}

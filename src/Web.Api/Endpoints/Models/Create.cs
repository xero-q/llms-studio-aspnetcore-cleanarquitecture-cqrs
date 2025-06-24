using Application.Models.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Models;

internal sealed class Create:IEndpoint
{
    public sealed class Request
    {
        public string Name { get; init; }
    
        public string Identifier { get; init; }

        public double Temperature { get; init; }
    
        public string EnvironmentVariable { get; init; }
    
        public int ModelTypeId { get; init; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiConstants.Create, async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateModelCommand
                {
                    Name = request.Name,
                    Identifier = request.Identifier,
                    Temperature = request.Temperature,
                    EnvironmentVariable = request.EnvironmentVariable,
                    ModelTypeId = request.ModelTypeId
                };

                Result<int> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Models)
            .RequireAuthorization();
    }
}

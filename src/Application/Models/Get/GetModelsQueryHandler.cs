using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Models.Get;

internal sealed class GetModelsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetModelsQuery, List<ModelResponse>>
{
    public async Task<Result<List<ModelResponse>>> Handle(GetModelsQuery query, CancellationToken cancellationToken)
    {
        List<ModelResponse> models = await context.Models
            .Include(m=>m.Provider)
            .Select(model => new ModelResponse
            {
                Id = model.Id,
                Name = model.Name,
                Identifier = model.Identifier,
                ModelType = model.Provider.Name,
            })
            .ToListAsync(cancellationToken);

        return models;
    }
}

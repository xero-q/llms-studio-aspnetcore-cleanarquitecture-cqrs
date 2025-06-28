using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.ModelTypes.Get;

internal sealed class GetModelTypesQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetModelTypesQuery, List<ModelTypeResponse>>
{
    public async Task<Result<List<ModelTypeResponse>>> Handle(GetModelTypesQuery query, CancellationToken cancellationToken)
    {
        List<ModelTypeResponse> modelTypes = await context.ModelTypes
            .AsNoTracking()
            .Select(modelType => new ModelTypeResponse
            {
                Id = modelType.Id,
                Name = modelType.Name
            })
            .ToListAsync(cancellationToken);

        return modelTypes;
    }
}

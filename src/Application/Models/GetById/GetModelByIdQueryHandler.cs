using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Models.GetById;

internal sealed class GetModelByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetModelByIdQuery, ModelResponse>
{
    public async Task<Result<ModelResponse>> Handle(GetModelByIdQuery query, CancellationToken cancellationToken)
    {
        ModelResponse? model = await context.Models
            .Include(m=>m.Provider)
            .Where(model => model.Id == query.ModelId)
            .Select(model => new ModelResponse
            {
                Id = model.Id,
                Name = model.Name,
                Identifier = model.Identifier,
                ModelType = model.Provider.Name
                
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (model is null)
        {
            return Result.Failure<ModelResponse>(ModelErrors.NotFound(query.ModelId));
        }

        return model;
    }
}

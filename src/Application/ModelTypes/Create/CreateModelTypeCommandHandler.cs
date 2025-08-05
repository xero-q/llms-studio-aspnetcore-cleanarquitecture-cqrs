using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.ModelTypes.Get;
using Domain.ModelTypes;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.ModelTypes.Create;

internal sealed class CreateModelTypeCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreateModelTypeCommand, ModelTypeResponse>
{
    public async Task<Result<ModelTypeResponse>> Handle(CreateModelTypeCommand command, CancellationToken cancellationToken)
    {
        ModelType? modelTypeFound = await context.ModelTypes.AsNoTracking().SingleOrDefaultAsync(m => EF.Functions.ILike(m.Name,command.Name), cancellationToken);

        if (modelTypeFound is not null)
        {
            return Result.Failure<ModelTypeResponse>(ModelTypeErrors.ModelTypeAlreadyExists(modelTypeFound.Name)); 
        }
        
        var modelTypeItem = new ModelType
        {
            Name = command.Name
        };

        modelTypeItem.Raise(new ModelTypeCreatedDomainEvent(modelTypeItem.Id));

        context.ModelTypes.Add(modelTypeItem);

        await context.SaveChangesAsync(cancellationToken);

        return new ModelTypeResponse
        {
            Id = modelTypeItem.Id,
            Name = modelTypeItem.Name
        };
    }
}

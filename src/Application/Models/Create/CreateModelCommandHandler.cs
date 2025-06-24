using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models;
using Domain.ModelTypes;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Models.Create;

internal sealed class CreateModelCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreateModelCommand, int>
{
    public async Task<Result<int>> Handle(CreateModelCommand command, CancellationToken cancellationToken)
    {
        ModelType? modelType = await context.ModelTypes
            .Where(mt =>mt.Id == command.ModelTypeId)
            .SingleOrDefaultAsync(cancellationToken);
        

        if (modelType == null)
        {
            return Result.Failure<int>(ModelTypeErrors.NotFound(command.ModelTypeId));
        }

        bool hasModel = context.Models.AsEnumerable()
            .Any(m => m.Identifier.Equals(command.Identifier, StringComparison.OrdinalIgnoreCase));

        if (hasModel)
        {
            return Result.Failure<int>(ModelErrors.IdentifierAlreadyExists(command.Identifier)); 
        }
        
        var modelItem = new Model
        {
            Name = command.Name,
            Identifier = command.Identifier,
            Temperature = command.Temperature,
            EnvironmentVariable = command.EnvironmentVariable,
            ModelTypeId = command.ModelTypeId,
        };
       
        modelItem.Raise(new ModelCreatedDomainEvent(modelItem.Id));

        context.Models.Add(modelItem);

        await context.SaveChangesAsync(cancellationToken);

        return modelItem.Id;
       
    }
}

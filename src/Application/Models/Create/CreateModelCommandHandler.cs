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
            .AsNoTracking()
            .Where(mt =>mt.Id == command.ModelTypeId)
            .SingleOrDefaultAsync(cancellationToken);
        

        if (modelType == null)
        {
            return Result.Failure<int>(ModelTypeErrors.NotFound(command.ModelTypeId));
        }

        Model? modelFound = null;
        
        modelFound = await context.Models.SingleOrDefaultAsync(m => EF.Functions.ILike(m.Identifier,command.Identifier), cancellationToken);

        if (modelFound != null)
        {
            return Result.Failure<int>(ModelErrors.IdentifierAlreadyExists(modelFound.Identifier)); 
        }
        
        modelFound = await context.Models.SingleOrDefaultAsync(m => EF.Functions.ILike(m.EnvironmentVariable,command.EnvironmentVariable),cancellationToken);

        if (modelFound != null)
        {
            return Result.Failure<int>(ModelErrors.EnvironmentVariableAlreadyExists(modelFound.EnvironmentVariable)); 
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

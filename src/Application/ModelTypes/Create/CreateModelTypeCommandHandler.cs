using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.ModelTypes;
using SharedKernel;

namespace Application.ModelTypes.Create;

internal sealed class CreateModelTypeCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreateModelTypeCommand, int>
{
    public async Task<Result<int>> Handle(CreateModelTypeCommand command, CancellationToken cancellationToken)
    {
       var modelTypeItem = new ModelType
        {
            Name = command.Name
        };

        modelTypeItem.Raise(new ModelTypeCreatedDomainEvent(modelTypeItem.Id));

        context.ModelTypes.Add(modelTypeItem);

        await context.SaveChangesAsync(cancellationToken);

        return modelTypeItem.Id;
    }
}

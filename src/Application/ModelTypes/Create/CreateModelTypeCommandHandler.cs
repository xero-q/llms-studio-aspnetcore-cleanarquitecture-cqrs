using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.ModelTypes.Get;
using Domain.ModelTypes;
using SharedKernel;

namespace Application.ModelTypes.Create;

internal sealed class CreateModelTypeCommandHandler(
    IApplicationDbContext context)
    : ICommandHandler<CreateModelTypeCommand, ModelTypeResponse>
{
    public async Task<Result<ModelTypeResponse>> Handle(CreateModelTypeCommand command, CancellationToken cancellationToken)
    {
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

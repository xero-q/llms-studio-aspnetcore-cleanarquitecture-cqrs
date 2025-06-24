using Application.Abstractions.Messaging;
using Application.ModelTypes.Get;

namespace Application.ModelTypes.Create;

public sealed class CreateModelTypeCommand : ICommand<ModelTypeResponse>
{
    public string Name { get; set; }
}

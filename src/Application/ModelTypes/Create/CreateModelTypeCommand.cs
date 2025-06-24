using Application.Abstractions.Messaging;

namespace Application.ModelTypes.Create;

public sealed class CreateModelTypeCommand : ICommand<int>
{
    public string Name { get; set; }
}

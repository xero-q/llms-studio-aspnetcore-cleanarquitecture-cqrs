using Application.Abstractions.Messaging;

namespace Application.Threads.Create;

public sealed class CreateThreadCommand : ICommand<int>
{
    public string Title { get; set; }
    
    public int ModelId { get; set; }
}

using Application.Abstractions.Messaging;
using Application.Prompts.Get;

namespace Application.Prompts.Create;

public sealed class CreatePromptCommand : ICommand<PromptResponse>
{
    public string PromptText { get; set; }
    
    public int ThreadId { get; set; }
}

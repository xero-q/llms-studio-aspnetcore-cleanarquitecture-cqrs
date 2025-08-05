using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public abstract class ModelAI(Thread thread) : IModelAI
{
    protected Thread Thread { get; } = thread;

    public abstract Task<string?> SendPrompt(string prompt);
}

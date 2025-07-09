using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public abstract class ModelAI:IModelAI
{
    protected Thread Thread { get; init; }
    protected IConfiguration Config { get; init; }
    
    protected ModelAI(Thread thread, IConfiguration config)
    {
       Thread = thread; 
       Config = config;
    }
    public abstract Task<string?> SendPrompt(string prompt);
}

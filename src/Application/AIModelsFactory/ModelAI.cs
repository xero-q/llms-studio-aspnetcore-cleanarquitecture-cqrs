using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public abstract class ModelAI:IModelAI
{
    protected Thread Thread { get; }
    
    protected ModelAI(Thread thread, IConfiguration config)
    {
       Thread = thread; 
    }
    public abstract Task<string?> SendPrompt(string prompt);
}

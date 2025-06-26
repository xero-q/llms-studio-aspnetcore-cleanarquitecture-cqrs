using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public abstract class ModelAI:IModelAI
{
    protected readonly Thread _thread;
    protected readonly IConfiguration _config;
    
    protected ModelAI(Thread thread, IConfiguration config)
    {
       _thread = thread; 
       _config = config;
    }
    public abstract Task<string?> SendPrompt(string prompt);
    
    public Thread Thread =>_thread;
    public IConfiguration Config => _config;

}

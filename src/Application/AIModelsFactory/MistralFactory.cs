using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public class MistralFactory:IModelAIFactory
{
    public IModelAI CreateModelAI(Thread thread, IConfiguration config)
    {
        return new ModelMistralAI(thread, config);
    }
}

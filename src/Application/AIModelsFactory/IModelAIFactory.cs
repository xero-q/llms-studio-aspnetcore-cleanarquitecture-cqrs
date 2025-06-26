using Microsoft.Extensions.Configuration;
using Thread = Domain.Threads.Thread;
namespace Application.AIModelsFactory;

public interface IModelAIFactory
{
    IModelAI CreateModelAI(Thread thread, IConfiguration config);
}

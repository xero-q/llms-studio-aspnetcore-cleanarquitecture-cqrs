using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Domain.Prompts;

public class Prompt:Entity
{
    
    public int Id { get; set; }
    public string PromptText { get; set; }
    
    public string Response { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int ThreadId { get; set; }

    public Thread Thread { get; set; } = null!;
}

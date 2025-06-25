using Domain.ModelTypes;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Domain.Models;

public class Model:Entity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Identifier { get; set; }

    public double Temperature { get; set; } = 0.7;
    
    public string EnvironmentVariable { get; set; }
    
    public int ModelTypeId { get; set; }

    public ModelType Provider { get; set; } = null!;
    
    public List<Thread> Threads { get; set; } = new();
}

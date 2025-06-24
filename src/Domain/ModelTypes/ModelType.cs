using Domain.Models;
using SharedKernel;

namespace Domain.ModelTypes;

public sealed class ModelType : Entity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Model> Models { get; set; } = new();
}

using Application.Abstractions.Messaging;

namespace Application.Models.Create;

public sealed class CreateModelCommand : ICommand<int>
{
    public string Name { get; set; }
    
    public string Identifier { get; set; }
    
    
    public double Temperature { get; set; } = 0.7;
    
    public string EnvironmentVariable { get; set; }
    
    public int ModelTypeId { get; set; }
}

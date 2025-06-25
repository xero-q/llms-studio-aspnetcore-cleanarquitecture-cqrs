using Domain.Models;
using Domain.Users;
using SharedKernel;

namespace Domain.Threads;

public class Thread:Entity
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int ModelId { get; set; }

    public Model Model { get; set; } = null!;

    public int UserId { get; set; }

    public User User { get; set; } = null!;
    
    // public List<Prompt> Prompts { get; set; } = new();
}

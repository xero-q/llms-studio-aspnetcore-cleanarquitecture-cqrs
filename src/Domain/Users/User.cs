using Domain.RefreshTokens;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Domain.Users;

public sealed class User : Entity
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    
    public List<Thread> Threads { get; set; } = new();
    
    public List<RefreshToken> RefreshTokens { get; set; } = new();
    
    
}

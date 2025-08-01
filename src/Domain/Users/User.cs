using Domain.RefreshTokens;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Domain.Users;

public sealed class User : Entity
{
    public int Id { get; init; }
    
    public string Username { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PasswordHash { get; init; }
    
    public List<Thread> Threads { get; init; } = [];
    
    public List<RefreshToken> RefreshTokens { get; init; } = [];
    
    
}

using Domain.Users;
using SharedKernel;

namespace Domain.RefreshTokens;

public class RefreshToken: Entity
{
    public int Id { get; set; }
    
    public string Token { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;
}

using Domain.ModelTypes;
using Domain.Users;
using Domain.Models;
using Domain.Prompts;
using Microsoft.EntityFrameworkCore;

using Thread = Domain.Threads.Thread;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<ModelType> ModelTypes { get; }
    
    DbSet<Model> Models { get; }
    
    DbSet<Thread> Threads { get; }
    
    DbSet<Prompt> Prompts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

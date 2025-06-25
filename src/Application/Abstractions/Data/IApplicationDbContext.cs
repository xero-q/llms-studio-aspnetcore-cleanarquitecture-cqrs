using Domain.ModelTypes;
using Domain.Todos;
using Domain.Users;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

using Thread = Domain.Threads.Thread;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    
    DbSet<ModelType> ModelTypes { get; }
    
    DbSet<Model> Models { get; }
    
    DbSet<Thread> Threads { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

using Domain.ModelTypes;
using Domain.Todos;
using Domain.Users;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    
    DbSet<ModelType> ModelTypes { get; }
    
    DbSet<Model> Models { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

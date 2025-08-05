using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Models;
using Domain.Threads;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.Threads.Create;

internal sealed class CreateThreadCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IUserContext userContext)
    : ICommandHandler<CreateThreadCommand, int>
{
    public async Task<Result<int>> Handle(CreateThreadCommand command, CancellationToken cancellationToken)
    {
       Model? model = await context.Models
           .AsNoTracking() 
           .Where(m =>m.Id == command.ModelId)
            .SingleOrDefaultAsync(cancellationToken);
        

        if (model is null)
        {
            return Result.Failure<int>(ThreadErrors.NotFound(command.ModelId));
        }
        
        //Verify not same thread title for the same user
        bool threadFound = await context.Threads
            .AnyAsync(t=>EF.Functions.ILike(t.Title, command.Title) && t.UserId == userContext.UserId, cancellationToken);

        if (threadFound)
        {
            return Result.Failure<int>(ThreadErrors.TitleAlreadyExistsSameUser(command.Title)); 
        }
        
        var thread = new Thread
        {
            Title = command.Title,
            ModelId = command.ModelId,
            UserId = userContext.UserId,
            CreatedAt = dateTimeProvider.UtcNow,
         
        };
       
        thread.Raise(new ThreadCreatedDomainEvent(thread.Id));

        context.Threads.Add(thread);

        await context.SaveChangesAsync(cancellationToken);

        return thread.Id;
       
    }
}

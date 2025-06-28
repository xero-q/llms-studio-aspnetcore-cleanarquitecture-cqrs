using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Threads;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.Threads.Delete;

internal sealed class DeleteThreadCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteThreadCommand>
{
    public async Task<Result> Handle(DeleteThreadCommand command, CancellationToken cancellationToken)
    {
        Thread? thread = await context.Threads
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == command.ThreadId, cancellationToken);

        if (thread is null)
        {
            return Result.Failure(ThreadErrors.NotFound(command.ThreadId));
        }

        context.Threads.Remove(thread);

        thread.Raise(new ThreadDeletedDomainEvent(thread.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

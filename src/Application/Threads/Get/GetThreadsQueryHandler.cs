using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.Threads.Get;

internal sealed class GetThreadsQueryHandler(IApplicationDbContext context, IUserContext userContext)
    : IQueryHandler<GetThreadsQuery, PaginatedThreadResponse>
{
    public async Task<Result<PaginatedThreadResponse>> Handle(GetThreadsQuery query, CancellationToken cancellationToken)
    {
        List<ThreadResponse> threads = await context.Threads
            .Include(t => t.Model)
            .ThenInclude(m => m.Provider)
            .Where(t => t.UserId == userContext.UserId)
            .OrderByDescending(t => t.CreatedAt)
            .Skip((query.pageNumber - 1) * query.pageSize)
            .Take(query.pageSize)
            .Select(t=>new ThreadResponse
            {
                Id = t.Id,
                ModelId= t.Model.Id,
                Title = t.Title,
                CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(
                    t.CreatedAt,
                    TimeZoneInfo.Local
                ),
                CreatedAtDate = TimeZoneInfo.ConvertTimeFromUtc(
                    t.CreatedAt,
                    TimeZoneInfo.Local
                ).ToString("yyyy-MM-dd",new CultureInfo("en-US")),
                ModelName = t.Model.Name,
                ModelType = t.Model.Provider.Name,
                ModelIdentifier = t.Model.Identifier
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        IQueryable<Thread> queryTotalThreads = context.Threads
            .Where(t => t.UserId == userContext.UserId)
            .AsNoTracking();

        int totalThreads = await queryTotalThreads.CountAsync(cancellationToken);
        
        var threadsGrouped = threads
            .GroupBy(t => t.CreatedAt.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US"))) 
            .ToDictionary(g => g.Key, g => g.ToList());
        
        bool hasNextPage = query.pageNumber * query.pageSize < totalThreads;

        return new PaginatedThreadResponse
        {
            CurrentPage = query.pageNumber,
            HasNext = hasNextPage,
            Results =  threadsGrouped.Select(x=>new ThreadListResponse
            {
                Date = x.Key,
                Threads = x.Value
            })
        };
        
    }
}

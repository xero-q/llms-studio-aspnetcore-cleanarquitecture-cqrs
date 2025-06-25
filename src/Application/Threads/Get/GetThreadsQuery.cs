using Application.Abstractions.Messaging;

namespace Application.Threads.Get;

public sealed record GetThreadsQuery(int pageNumber, int pageSize) : IQuery<PaginatedThreadResponse>;

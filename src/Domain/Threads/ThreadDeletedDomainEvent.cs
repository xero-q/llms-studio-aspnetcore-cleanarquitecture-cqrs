using SharedKernel;

namespace Domain.Threads;

public sealed record ThreadDeletedDomainEvent(int ThreadId) : IDomainEvent;

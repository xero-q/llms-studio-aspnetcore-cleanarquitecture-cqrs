using SharedKernel;

namespace Domain.Threads;

public sealed record ThreadCreatedDomainEvent(int threadId) : IDomainEvent;

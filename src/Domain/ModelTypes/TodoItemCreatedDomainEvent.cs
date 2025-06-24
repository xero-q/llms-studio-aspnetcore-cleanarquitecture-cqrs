using SharedKernel;

namespace Domain.ModelTypes;

public sealed record ModelTypeCreatedDomainEvent(int modelTypeId) : IDomainEvent;

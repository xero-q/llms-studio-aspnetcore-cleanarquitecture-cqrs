using SharedKernel;

namespace Domain.Models;

public sealed record ModelCreatedDomainEvent(int modelId) : IDomainEvent;

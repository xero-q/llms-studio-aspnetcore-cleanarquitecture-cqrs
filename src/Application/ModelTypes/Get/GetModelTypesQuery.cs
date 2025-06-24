using Application.Abstractions.Messaging;

namespace Application.ModelTypes.Get;

public sealed record GetModelTypesQuery : IQuery<List<ModelTypeResponse>>;

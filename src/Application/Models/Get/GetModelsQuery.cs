using Application.Abstractions.Messaging;

namespace Application.Models.Get;

public sealed record GetModelsQuery : IQuery<List<ModelResponse>>;

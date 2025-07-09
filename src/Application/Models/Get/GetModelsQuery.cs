using Application.Abstractions.Messaging;

namespace Application.Models.GetById;

public sealed record GetModelsQuery : IQuery<List<ModelResponse>>;

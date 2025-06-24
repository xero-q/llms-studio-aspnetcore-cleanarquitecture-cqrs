using Application.Abstractions.Messaging;

namespace Application.Models.GetById;

public sealed record GetModelByIdQuery(int ModelId) : IQuery<ModelResponse>;

using Application.Abstractions.Messaging;

namespace Application.Prompts.Get;

public sealed record GetPromptsQuery(int ThreadId) : IQuery<List<PromptResponse>>;

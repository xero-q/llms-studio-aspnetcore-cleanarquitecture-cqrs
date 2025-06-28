using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Prompts.Get;

internal sealed class GetPromptsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetPromptsQuery, List<PromptResponse>>
{
    public async Task<Result<List<PromptResponse>>> Handle(GetPromptsQuery query, CancellationToken cancellationToken)
    {
        List<PromptResponse> prompts = await context.Prompts
            .AsNoTracking()
            .Where(p => p.ThreadId == query.ThreadId)
            .OrderBy(p => p.CreatedAt)
            .Select(p=>new PromptResponse
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Prompt = p.PromptText,
                Response = p.Response,
                ThreadId = p.ThreadId
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return prompts;

    }
}

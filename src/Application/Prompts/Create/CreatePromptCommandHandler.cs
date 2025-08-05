using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.AIModelsFactory;
using Application.Prompts.Get;
using Domain.Prompts;
using Domain.Threads;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.Prompts.Create;

internal sealed class CreatePromptCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IConfiguration config
)
    : ICommandHandler<CreatePromptCommand, PromptResponse>
{
    public async Task<Result<PromptResponse>> Handle(CreatePromptCommand command, CancellationToken cancellationToken)
    {
        Thread? thread = await context.Threads.AsNoTracking().Include(t=>t.Prompts).Include(t=>t.Model).ThenInclude(m=>m.Provider).FirstOrDefaultAsync(t=>t.Id == command.ThreadId, cancellationToken);

        if (thread is null)
        {
            return Result.Failure<PromptResponse>(ThreadErrors.NotFound(command.ThreadId));
        }

        string provider = thread.Model.Provider.Name;

        IModelAIFactory? modelAIFactory = provider switch
        {
            "gemini" => new GeminiFactory(),
            "deepseek" => new DeepSeekFactory(),
            "mistral" => new MistralFactory(),
            _ => null
        };

        if (modelAIFactory is null)
        {
            return Result.Failure<PromptResponse>(PromptErrors.AIModelNotFound(provider));
        }

        IModelAI modelAI = modelAIFactory.CreateModelAI(thread, config);


        try
        {
            string? response = await modelAI.SendPrompt(command.PromptText);

            if (response is null)
            {
                return Result.Failure<PromptResponse>(PromptErrors.CouldNotGetResponse());
            }
            
            var newPrompt = new Prompt
            {
                PromptText = command.PromptText,
                Response = response,
                ThreadId = command.ThreadId,
                CreatedAt = dateTimeProvider.UtcNow
            };

            context.Prompts.Add(newPrompt);

            await context.SaveChangesAsync(cancellationToken);

            return new PromptResponse
            {
                Id = newPrompt.Id,
                Prompt = newPrompt.PromptText,
                Response = newPrompt.Response,
                CreatedAt = newPrompt.CreatedAt,
                ThreadId = newPrompt.ThreadId
            };
        }
        catch
        {
            return Result.Failure<PromptResponse>(PromptErrors.CouldNotGetResponse());
        }
    }
}

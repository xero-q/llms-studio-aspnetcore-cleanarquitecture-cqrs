using SharedKernel;

namespace Domain.Prompts;

public static class PromptErrors
{
    
    public static Error AIModelNotFound(string provider) => Error.Validation(
        "Prompts.ProviderNotHandled",
        $"The system cannot process this Provider = '{provider}'");
    
    public static Error CouldNotGetResponse() => Error.Failure(
        "Prompts.CouldNotGetLLMResponse",
        $"The system could not get the response from the LLM");
    
}

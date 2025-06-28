using SharedKernel;

namespace Domain.Threads;

public static class ThreadErrors
{
    public static Error NotFound(int modelId) => Error.NotFound(
        "Models.NotFound",
        $"The Model with the Id = '{modelId}' was not found");
    public static Error TitleAlreadyExistsSameUser(string threadTitle) => Error.Validation(
        "Threads.ThreadSameTitleAlreadyExists",
        $"You already have a Thread with Title '{threadTitle}'");
    
}

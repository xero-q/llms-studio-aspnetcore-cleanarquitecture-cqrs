using SharedKernel;

namespace Domain.Threads;

public static class ThreadErrors
{
    public static Error NotFound(int modelId) => Error.NotFound(
        "Models.NotFound",
        $"The Model with the Id = '{modelId}' was not found");
    
    public static Error IdentifierAlreadyExists(string modelTypeIdentifier) => Error.Validation(
        "Models.IdentifierAlreadyExists",
        $"The Model with Identifier = '{modelTypeIdentifier}' already exists");
    
    public static Error EnvironmentVariableAlreadyExists(string modelTypeEnvironmentVariable) => Error.Validation(
        "Models.EnvironmentVariableAlreadyExists",
        $"The Model with EnvironmentVariable = '{modelTypeEnvironmentVariable}' already exists");
    
}

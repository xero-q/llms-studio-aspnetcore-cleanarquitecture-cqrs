using SharedKernel;

namespace Domain.Models;

public static class ModelErrors
{
    public static Error IdentifierAlreadyExists(string modelTypeIdentifier) => Error.Validation(
        "Models.IdentifierAlreadyExists",
        $"The Model with Identifier = '{modelTypeIdentifier}' already exists");
    
    public static Error EnvironmentVariableAlreadyExists(string modelTypeEnvironmentVariable) => Error.Validation(
        "Models.EnvironmentVariableAlreadyExists",
        $"The Model with EnvironmentVariable = '{modelTypeEnvironmentVariable}' already exists");
    
}

using SharedKernel;

namespace Domain.Models;

public static class ModelErrors
{
    public static Error IdentifierAlreadyExists(string modelTypeIdentifier) => Error.Conflict(
        "Models.IdentifierAlreadyExists",
        $"The Model with Identifier = '{modelTypeIdentifier}' already exists");
    
}

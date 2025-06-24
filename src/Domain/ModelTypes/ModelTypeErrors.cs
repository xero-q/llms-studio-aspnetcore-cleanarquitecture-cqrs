using SharedKernel;

namespace Domain.ModelTypes;

public static class ModelTypeErrors
{
    public static Error NotFound(int modelTypeId) => Error.NotFound(
        "ModelTypes.NotFound",
        $"The ModelType with the Id = '{modelTypeId}' was not found");
    
}

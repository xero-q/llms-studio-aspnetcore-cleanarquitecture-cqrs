using SharedKernel;

namespace Domain.ModelTypes;

public static class ModelTypeErrors
{
    public static Error NotFound(int modelTypeId) => Error.NotFound(
        "ModelTypes.NotFound",
        $"The ModelType with the Id = '{modelTypeId}' was not found");
    
    public static Error ModelTypeAlreadyExists(string modelTypeName) => Error.Validation(
        "ModelTypes.NameAlreadyExists",
        $"The ModelType with Name = '{modelTypeName}' already exists");
    
}

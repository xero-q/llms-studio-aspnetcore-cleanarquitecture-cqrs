namespace SharedKernel;

public static class ErrorMessages
{
    public static string PropertyCanNotBeEmpty(string propertyName)
    {
        return $"'{propertyName}' cannot be empty";
    } 
    public const string TemperatureInvalidValue = "The temperature must be between 0.0 and 1.0.";

    public const string ErrorRequestLLM = "Error while querying LLM";
}

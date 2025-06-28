namespace Web.Api.Endpoints.Models;

public static class ApiConstants
{
   private const string Base = "api/models";
   public const string Create = Base;
   public const string Get = Base;
   public const string GetById = $"{Base}/{{modelId:int}}";
}

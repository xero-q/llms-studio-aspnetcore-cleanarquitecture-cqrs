namespace Web.Api.Endpoints.Threads;

public static class ApiConstants
{
   private const string Base = "api/threads";
   public const string Create = $"{Web.Api.Endpoints.Models.ApiConstants.Create}/{{modelId:int}}/threads";
   public const string Get = Base;
   public const string GetById = $"{Base}/{{threadId:int}}";
   public const string Delete = $"{Base}/{{threadId:int}}";
}

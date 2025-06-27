namespace Web.Api.Endpoints.Threads;

public static class ApiConstants
{
   private const string Base = "api/threads";
   public const string Create = $"{Web.Api.Endpoints.Models.ApiConstants.Create}/{{id:int}}/threads";
   public const string Get = Base;
   public const string GetById = $"{Base}/{{id:int}}";
   public const string Delete = $"{Base}/{{id:int}}";
}

namespace Web.Api.Endpoints.Prompts;

public static class ApiConstants
{
   private const string Base = $"{Web.Api.Endpoints.Threads.ApiConstants.GetById}/prompts";
   public const string Create = Base;
   public const string Get = Base;
   public const string GetById = $"{Base}/{{id:int}}";
}

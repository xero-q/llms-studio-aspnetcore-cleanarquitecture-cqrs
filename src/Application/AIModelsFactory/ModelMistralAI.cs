using System.Text;
using Domain.Prompts;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.AIModelsFactory;

public class ModelMistralAI(Thread thread, IConfiguration config) : ModelAI(thread, config)
{
    
    private readonly string? Url = config["Models:Mistral:Url"];

    public override async Task<string?> SendPrompt(string prompt)
    {
        Env.Load();

        string apiKeyName = Thread.Model.EnvironmentVariable;

        string? apiKey = Environment.GetEnvironmentVariable(apiKeyName);

        if (apiKey == null)
        {
            return null;
        }

        string modelIdentifier = Thread.Model.Identifier;

        List<Prompt> prompts = Thread.Prompts;

        var messages = new List<object>();

        foreach (Prompt promptRecord in prompts)
        {
            messages.Add(new
            {
                role = "user",
                content = promptRecord.PromptText
            });
            
            messages.Add(new
            {
                role = "assistant",
                content = promptRecord.Response
            });
        }
        
        messages.Add(new
        {
            role = "user",
            content = prompt
        });
        
        var payload = new
        {
            model = modelIdentifier,
            messages = messages.ToArray(),
            temperature = Thread.Model.Temperature,
            stream = false
        };


        using var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        using var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(Url, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var responseJson = JObject.Parse(responseBody);


            string? text = (string)responseJson["choices"]?[0]?["message"]?["content"];

            return text;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ErrorMessages.ErrorRequestLLM}: {ex.Message}");
        }
    }
}

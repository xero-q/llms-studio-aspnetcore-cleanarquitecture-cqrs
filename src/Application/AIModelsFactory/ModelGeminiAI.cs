using System.Text;
using Domain.Prompts;
using Thread = Domain.Threads.Thread;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedKernel;

namespace Application.AIModelsFactory;

public class ModelGeminiAI(Thread thread, IConfiguration config) : ModelAI(thread, config)
{
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

        string url =
            $"https://generativelanguage.googleapis.com/v1beta/models/{modelIdentifier}:generateContent?key={apiKey}";

        using var httpClient = new HttpClient();

        List<Prompt> prompts = Thread.Prompts;

        var contents = new List<object>();

        foreach (Prompt promptRecord in prompts)
        {
            contents.Add(new
            {
                role = "user",
                parts = new []
                {
                    new {text = promptRecord.PromptText}
                }
            });
            
            contents.Add(new
            {
                role = "model",
                parts = new []
                {
                    new {text = promptRecord.Response}
                }
            });
        }
        
        contents.Add(new
        {
            role = "user",
            parts = new []
            {
                new {text = prompt}
            }
        });
        
        var payload = new
        {
            contents = contents.ToArray(),
            generationConfig = new
            {
                temperature = Thread.Model.Temperature
            }
        };

        using var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var responseJson = JObject.Parse(responseBody);


            string? text = (string)responseJson["candidates"]?[0]?["content"]?["parts"]?[0]?["text"];

            return text;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ErrorMessages.ErrorRequestLLM}: {ex.Message}");
        }
    }
}

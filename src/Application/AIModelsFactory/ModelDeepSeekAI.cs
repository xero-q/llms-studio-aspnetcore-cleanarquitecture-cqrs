using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedKernel;
using Thread = Domain.Threads.Thread;

namespace Application.AIModelsFactory;

public class ModelDeepSeekAI(Thread thread, IConfiguration config) : ModelAI(thread, config)
{
    private readonly string? Url = config["Models:DeepSeekAI:Url"];

    public override async Task<string?> SendPrompt(string prompt)
    {
        string apiKeyName = Thread.Model.EnvironmentVariable;

        string? apiKey = Environment.GetEnvironmentVariable(apiKeyName);

        if (apiKey == null)
        {
            return null;
        }


        string modelIdentifier = Thread.Model.Identifier;

        var payload = new
        {
            model = modelIdentifier,
            messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt },
            },
            stream = false,
            temperature = Thread.Model.Temperature,
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


            string? text = (string)(responseJson?["choices"]?[0]?["message"]?["content"]);

            return text;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ErrorMessages.ErrorRequestLLM}: {ex.Message}");
        }
    }
}

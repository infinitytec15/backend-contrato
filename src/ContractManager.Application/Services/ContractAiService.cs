using ContractManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ContractManager.Application.Services;

public class ContractAiService : IContractAiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ContractAiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> AnalisarContratoAsync(string contrato)
    {
        var prompt = $"Resuma e destaque pontos importantes deste contrato:\n\n{contrato}";
        return await GerarRespostaAsync(prompt);
    }

    public async Task<string> GerarContratoAsync(string prompt)
    {
        return await GerarRespostaAsync(prompt);
    }

    private async Task<string> GerarRespostaAsync(string prompt)
    {
        var apiKey = _config["OpenAI:ApiKey"];
        var endpoint = "https://api.openai.com/v1/chat/completions";

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var payload = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(json)!;

        return result.choices[0].message.content;
    }
}
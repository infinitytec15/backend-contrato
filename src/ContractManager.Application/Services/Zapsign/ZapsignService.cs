using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ContractManager.Application.Interfaces;

namespace ContractManager.Application.Services.Zapsign;

public class ZapsignService : IZapsignService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ZapsignService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        _httpClient.BaseAddress = new Uri("https://api.zapsign.com.br/api/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _config["Zapsign:Token"]);
    }

    public async Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent)
    {
        var payload = new
        {
            file_content_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(contractContent)),
            name = $"Contrato {contractId}",
            signers = new[]
            {
                new { email = signatoryEmail, action = "sign" }
            },
            external_id = contractId.ToString()
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("documents/", content);

        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return json; // ou extrair o ID do documento se desejar
    }
}
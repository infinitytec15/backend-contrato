using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ContractManager.Application.Interfaces;

namespace ContractManager.Application.Services.External;

public class ZapsignService : IZapsignService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiToken;

    public ZapsignService(IConfiguration config)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.zapsign.com.br/api/v1/")
        };

        _apiToken = config["Zapsign:Token"] ?? throw new Exception("Token da Zapsign não encontrado");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiToken);
    }

    public async Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent)
    {
        var payload = new
        {
            name = $"Contrato {contractId}",
            content_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(contractContent)),
            signers = new[]
            {
                new
                {
                    email = signatoryEmail,
                    action = "sign"
                }
            },
            external_id = contractId.ToString()
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("documents/base64/", content);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao enviar para assinatura: {erro}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}
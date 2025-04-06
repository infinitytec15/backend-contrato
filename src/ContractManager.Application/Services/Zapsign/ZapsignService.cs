using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ContractManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ContractManager.Application.Services.Zapsign;

public class ZapsignService : IZapsignService
{
    private readonly HttpClient _httpClient;
    private readonly string _zapsignToken;

    public ZapsignService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _zapsignToken = configuration["Zapsign:ApiKey"]
                        ?? throw new Exception("Zapsign API key não configurada.");
    }

    public async Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent)
    {
        var payload = new
        {
            sandbox = true,
            name = $"Contrato #{contractId}",
            content_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(contractContent)),
            signers = new[]
            {
                new
                {
                    email = signatoryEmail,
                    name = "Signatário",
                    lock_email = true,
                    send_email = true
                }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.zapsign.com.br/api/v1/documents")
        {
            Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Token", $"token={_zapsignToken}");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
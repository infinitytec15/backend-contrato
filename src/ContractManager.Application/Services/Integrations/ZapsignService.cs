using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ContractManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ContractManager.Application.Services.Integrations;

/// <summary>
/// Serviço para integração com a API da Zapsign (assinatura digital)
/// </summary>
public class ZapsignService : IZapsignService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ZapsignService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    public async Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent)
    {
        // Obtem as configurações do appsettings.json
        var apiToken = _configuration["Zapsign:Token"];
        var folderToken = _configuration["Zapsign:FolderToken"]; // opcional

        if (string.IsNullOrEmpty(apiToken))
            throw new InvalidOperationException("Zapsign API Token não foi configurado.");

        // Monta o payload da requisição
        var payload = new
        {
            name = $"Contrato #{contractId}",
            content_base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(contractContent)),
            signers = new[]
            {
                new
                {
                    email = signatoryEmail,
                    name = "Signatário",
                    action = "sign"
                }
            },
            external_id = contractId.ToString(),
            folder_token = folderToken // pode ser nulo se não quiser agrupar por pasta
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json"
        );

        // Configura a autenticação
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", apiToken);

        // Faz a chamada para a Zapsign
        var response = await _httpClient.PostAsync("https://api.zapsign.com.br/api/v1/documents/base64/", jsonContent);
        response.EnsureSuccessStatusCode();

        // Retorna a resposta da API da Zapsign (JSON)
        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ContractManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;

public class ZapsignService : IZapsignService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ZapsignService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["Zapsign:ApiKey"]!;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiKey);
        _httpClient.BaseAddress = new Uri("https://api.zapsign.com.br/api/v1/");
    }

    public async Task<string> EnviarParaAssinaturaAsync(string nomeArquivo, byte[] documento, string signatarioEmail, string signatarioNome)
    {
        var base64Documento = Convert.ToBase64String(documento);

        var payload = new
        {
            file_base64 = base64Documento,
            name = nomeArquivo,
            signers = new[]
            {
                new
                {
                    name = signatarioNome,
                    email = signatarioEmail,
                    identifier = signatarioEmail, // pode ser CPF também
                    send_automatic_email = true
                }
            },
            external_id = Guid.NewGuid().ToString(),
            lang = "pt_br"
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("documents/", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Erro ao enviar para assinatura Zapsign: {responseBody}");

        return responseBody;
    }
}
using ContractManager.Application.Interfaces;
using RestSharp;
using System.Text.Json;

namespace ContractManager.Application.Services.Zapsign;

public class ZapsignService : IZapsignService
{
    private readonly string _token = "SEU_TOKEN_AQUI"; // ⚠️ Ideal usar appsettings

    public async Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent)
    {
        var client = new RestClient("https://api.zapsign.com.br/api/v1/documents/");
        var request = new RestRequest(Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", $"Token {_token}");

        var body = new
        {
            name = $"Contrato #{contractId}",
            content_base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(contractContent)),
            signers = new[]
            {
                new {
                    email = signatoryEmail,
                    act = "sign",
                    send_email = true
                }
            }
        };

        request.AddStringBody(JsonSerializer.Serialize(body), DataFormat.Json);

        var response = await client.ExecuteAsync(request);
        return response.Content ?? throw new Exception("Erro ao enviar para Zapsign.");
    }
}
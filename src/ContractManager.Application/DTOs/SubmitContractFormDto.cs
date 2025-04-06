namespace ContractManager.Application.DTOs;

public class SubmitContractFormDto
{
    public Guid ContractId { get; set; }

    // ✅ Precisa conter a lista de respostas (campo/valor)
    public Dictionary<string, string> Responses { get; set; } = new();
}
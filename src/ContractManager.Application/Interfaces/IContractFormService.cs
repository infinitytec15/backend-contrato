using ContractManager.Application.DTOs;

namespace ContractManager.Application.Interfaces;

public interface IContractFormService
{
    Task SaveFormResponsesAsync(SubmitContractFormDto dto);

    Task<string> GenerateContractWithResponsesAsync(Guid contractId); // ✅ Adicione esta linha
}

using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class ContractFormService : IContractFormService
{
    private readonly IContractRepository _contractRepository;
    private readonly IContractFormRepository _formRepository;

    public ContractFormService(
        IContractRepository contractRepository,
        IContractFormRepository formRepository)
    {
        _contractRepository = contractRepository;
        _formRepository = formRepository;
    }

    /// <summary>
    /// Salva as respostas preenchidas de um formulário dinâmico.
    /// </summary>
    public async Task SaveFormResponsesAsync(SubmitContractFormDto dto)
    {
        var contract = await _contractRepository.GetByIdAsync(dto.ContractId);
        if (contract == null)
            throw new Exception("Contrato não encontrado");

        foreach (var entry in dto.Responses)
        {
            var response = new ContractFormResponse(
                contract.Id,
                entry.Key,
                entry.Value
            );

            contract.AttachFormResponse(response);
            await _formRepository.AddAsync(response);
        }

        await _contractRepository.UpdateAsync(contract); // Atualiza se for necessário
    }

    /// <summary>
    /// Gera o conteúdo final do contrato substituindo os campos dinâmicos preenchidos.
    /// </summary>
    public async Task<string> GenerateContractWithResponsesAsync(Guid contractId)
    {
        var contract = await _contractRepository.GetContractWithResponsesAsync(contractId);
        if (contract == null)
            throw new Exception("Contrato não encontrado.");

        var content = contract.Content;

        // Substitui os placeholders no formato {{campo}} com os valores respondidos
        foreach (var response in contract.FormResponses)
        {
            content = content.Replace($"{{{{{response.FieldKey}}}}}", response.FieldValue);
        }

        return content;
    }
}

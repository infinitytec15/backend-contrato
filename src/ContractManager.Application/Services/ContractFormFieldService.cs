using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class ContractFormFieldService : IContractFormFieldService
{
    private readonly IContractFormFieldRepository _repository;

    public ContractFormFieldService(IContractFormFieldRepository repository)
    {
        _repository = repository;
    }

    public async Task AddFieldAsync(CreateFormFieldDto dto)
    {
        var field = new ContractFormField(dto.ContractId, dto.FieldName, dto.FieldType, dto.IsRequired);
        await _repository.AddAsync(field);
    }

    public async Task<IEnumerable<CreateFormFieldDto>> GetFieldsAsync(Guid contractId)
    {
        var fields = await _repository.GetByContractIdAsync(contractId);

        return fields.Select(f => new CreateFormFieldDto
        {
            ContractId = f.ContractId,
            FieldName = f.FieldName,
            FieldType = f.FieldType,
            IsRequired = f.IsRequired
        });
    }
}
using ContractManager.Application.DTOs;

namespace ContractManager.Application.Interfaces;

public interface IContractFormFieldService
{
    Task AddFieldAsync(CreateFormFieldDto dto);
    Task<IEnumerable<CreateFormFieldDto>> GetFieldsAsync(Guid contractId);
}
using ContractManager.Domain.Entities;

namespace ContractManager.Domain.Interfaces;

public interface IContractFormFieldRepository
{
    Task AddAsync(ContractFormField field);
    Task<IEnumerable<ContractFormField>> GetByContractIdAsync(Guid contractId);
}
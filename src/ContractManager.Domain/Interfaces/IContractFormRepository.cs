using ContractManager.Domain.Entities;

namespace ContractManager.Domain.Interfaces;

public interface IContractFormRepository
{
    Task AddAsync(ContractFormResponse response);
    Task<IEnumerable<ContractFormResponse>> GetByContractIdAsync(Guid contractId);
}
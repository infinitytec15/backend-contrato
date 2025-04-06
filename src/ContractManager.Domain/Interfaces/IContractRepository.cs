using ContractManager.Domain.Entities;

namespace ContractManager.Domain.Interfaces;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id);
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<Contract> AddAsync(Contract contract);
    Task UpdateAsync(Contract contract);
    Task DeleteAsync(Guid id);
    Task<Contract?> GetContractWithResponsesAsync(Guid id); // precisa estar aqui
}

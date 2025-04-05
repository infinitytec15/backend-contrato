using ContractManager.Application.DTOs;
using ContractManager.Domain.Entities;

namespace ContractManager.Application.Interfaces;

public interface IContractService
{
    Task<Contract> CreateAsync(CreateContractDto dto);
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<Contract?> GetByIdAsync(Guid id);
    Task<bool> ApproveAsync(Guid id);
    Task<bool> RejectAsync(Guid id);

}
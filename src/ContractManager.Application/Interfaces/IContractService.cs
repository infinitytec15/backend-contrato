using ContractManager.Application.DTOs;
using ContractManager.Domain.Entities;

namespace ContractManager.Application.Interfaces;

public interface IContractService
{
    Task<Contract> CreateContractAsync(CreateContractDto dto);
    Task<IEnumerable<Contract>> GetAllContractsAsync();
    Task<Contract?> GetContractByIdAsync(Guid id);
}
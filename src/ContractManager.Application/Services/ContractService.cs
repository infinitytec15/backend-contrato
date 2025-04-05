using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;

    public ContractService(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<Contract> CreateContractAsync(CreateContractDto dto)
    {
        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            ClientId = dto.ClientId,
            CreatedAt = DateTime.UtcNow,
            ExpirationDate = dto.ExpirationDate
        };

        await _contractRepository.AddAsync(contract);
        return contract;
    }

    public async Task<IEnumerable<Contract>> GetAllContractsAsync()
    {
        return await _contractRepository.GetAllAsync();
    }

    public async Task<Contract?> GetContractByIdAsync(Guid id)
    {
        return await _contractRepository.GetByIdAsync(id);
    }
}
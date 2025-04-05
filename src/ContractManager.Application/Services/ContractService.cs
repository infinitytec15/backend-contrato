using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _repository;

    public ContractService(IContractRepository repository)
    {
        _repository = repository;
    }

    public async Task<Contract> CreateAsync(CreateContractDto dto)
    {
        var contract = Contract.Create(
            dto.Title,
            dto.Content,
            dto.ClientId,
            dto.ExpirationDate,
            dto.Tags.ToList() // 💡 Aqui estava o erro: garantindo que seja List<string>
        );

        await _repository.AddAsync(contract);
        return contract;
    }

    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Contract?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    public async Task<bool> ApproveAsync(Guid id)
    {
        var contract = await _repository.GetByIdAsync(id);
        if (contract == null) return false;

        contract.Approve();
        await _repository.UpdateAsync(contract);
        return true;
    }

    public async Task<bool> RejectAsync(Guid id)
    {
        var contract = await _repository.GetByIdAsync(id);
        if (contract == null) return false;

        contract.Reject();
        await _repository.UpdateAsync(contract);
        return true;
    }
    


}
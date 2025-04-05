using ContractManager.Domain.Entities;

namespace ContractManager.Domain.Interfaces;

public interface IClientRepository
{
    Task AddAsync(Client client);
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
}
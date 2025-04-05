using ContractManager.Domain.Entities;

namespace ContractManager.Domain.Interfaces;

public interface IDocumentRepository
{
    Task<Document> AddAsync(Document document);
    Task<IEnumerable<Document>> GetAllAsync();
    Task<Document?> GetByIdAsync(Guid id);
    Task<IEnumerable<Document>> GetByContractIdAsync(Guid contractId);
}
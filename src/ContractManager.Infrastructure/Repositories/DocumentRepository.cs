using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly AppDbContext _context;

    public DocumentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Document> AddAsync(Document document)
    {
        await _context.Documents.AddAsync(document);
        await _context.SaveChangesAsync();
        return document;
    }

    public async Task<IEnumerable<Document>> GetAllAsync()
    {
        return await _context.Documents.ToListAsync();
    }

    public async Task<Document?> GetByIdAsync(Guid id)
    {
        return await _context.Documents.FindAsync(id);
    }

    public async Task<IEnumerable<Document>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.Documents
            .Where(d => d.ContractId == contractId)
            .ToListAsync();
    }
}
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Repositories;

public class ContractFormRepository : IContractFormRepository
{
    private readonly AppDbContext _context;

    public ContractFormRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ContractFormResponse response)
    {
        await _context.ContractFormResponses.AddAsync(response);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContractFormResponse>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.ContractFormResponses
            .Where(r => r.ContractId == contractId)
            .ToListAsync();
    }
}
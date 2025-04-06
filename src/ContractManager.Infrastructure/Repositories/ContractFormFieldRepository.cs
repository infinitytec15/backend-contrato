using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;
using ContractManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Repositories;

public class ContractFormFieldRepository : IContractFormFieldRepository
{
    private readonly AppDbContext _context;

    public ContractFormFieldRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ContractFormField field)
    {
        await _context.ContractFormFields.AddAsync(field);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ContractFormField>> GetByContractIdAsync(Guid contractId)
    {
        return await _context.ContractFormFields
            .Where(f => f.ContractId == contractId)
            .ToListAsync();
    }
}
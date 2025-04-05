using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Plan> Plans => Set<Plan>();
}
using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // 📄 Contratos e Documentos
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Document> Documents => Set<Document>();

    // 👥 Clientes, Usuários e Planos
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Plan> Plans => Set<Plan>();

    // 🔔 Alertas do sistema
    public DbSet<Alert> Alerts => Set<Alert>();

    // 🧾 Formulários Dinâmicos
    public DbSet<ContractFormField> ContractFormFields => Set<ContractFormField>();
    public DbSet<ContractFormResponse> ContractFormResponses => Set<ContractFormResponse>();
}
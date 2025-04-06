using ContractManager.Application.Interfaces.Storage;
using ContractManager.Application.Services.Storage;
using ContractManager.Domain.Interfaces;
using ContractManager.Infrastructure.Persistence;
using ContractManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // 📦 Configuração do banco de dados PostgreSQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        // 🧱 Registro dos repositórios (DI)
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IContractFormFieldRepository, ContractFormFieldRepository>();
        services.AddScoped<IContractFormRepository, ContractFormRepository>();
        services.AddScoped<IZapsignService, ZapsignService>();


        // ☁️ Armazenamento de arquivos na nuvem (Wasabi/S3 compatível)
        services.AddSingleton<IDocumentStorageService, WasabiStorageService>();

        return services;
    }
}
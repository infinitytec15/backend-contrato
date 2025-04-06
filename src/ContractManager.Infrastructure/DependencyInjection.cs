using ContractManager.Application.Interfaces.Storage;
using ContractManager.Application.Interfaces;
using ContractManager.Application.Services.Storage;
using ContractManager.Application.Services.Zapsign;
using ContractManager.Application.Services.External;
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

        // 📂 Repositórios do domínio
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IContractFormFieldRepository, ContractFormFieldRepository>();
        services.AddScoped<IContractFormRepository, ContractFormRepository>();

        // ☁️ Armazenamento de arquivos (Wasabi - compatível com S3)
        services.AddSingleton<IDocumentStorageService, WasabiStorageService>();

        // 🔐 Integração Zapsign
        services.AddHttpClient(); // Necessário para serviços externos
        services.AddScoped<IZapsignService, ZapsignService>();

        // 🤖 Integração com ChatGPT/OpenAI
        services.AddHttpClient<IContractAiService, ContractAiService>();

        return services;
    }
}
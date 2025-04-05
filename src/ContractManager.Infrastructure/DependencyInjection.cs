using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContractManager.Domain.Interfaces;
using ContractManager.Infrastructure.Repositories;
using ContractManager.Infrastructure.Persistence;

namespace ContractManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContractRepository, ContractRepository>();

        return services;
    }
}
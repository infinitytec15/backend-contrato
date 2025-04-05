using Microsoft.Extensions.DependencyInjection;
using ContractManager.Application.Interfaces;
using ContractManager.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContractManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();
        return services;
    }
}
using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientResponseDto> CreateAsync(CreateClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _clientRepository.AddAsync(client);

        return new ClientResponseDto
        {
            Id = client.Id,
            Name = client.Name
        };
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(c => new ClientResponseDto
        {
            Id = c.Id,
            Name = c.Name
        });
    }

    public async Task<ClientResponseDto?> GetByIdAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null) return null;

        return new ClientResponseDto
        {
            Id = client.Id,
            Name = client.Name
        };
    }
}
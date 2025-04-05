using ContractManager.Application.DTOs;

namespace ContractManager.Application.Interfaces;

public interface IClientService
{
    Task<ClientResponseDto> CreateAsync(CreateClientDto dto);
    Task<IEnumerable<ClientResponseDto>> GetAllAsync();
    Task<ClientResponseDto?> GetByIdAsync(Guid id);
}
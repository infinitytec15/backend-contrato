using ContractManager.Application.DTOs;

namespace ContractManager.Application.Interfaces;

public interface IDocumentService
{
    Task<DocumentDto> UploadAsync(UploadDocumentDto dto);
    Task<IEnumerable<DocumentDto>> GetAllAsync();
    Task<DocumentDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<DocumentDto>> GetByContractIdAsync(Guid contractId);
}
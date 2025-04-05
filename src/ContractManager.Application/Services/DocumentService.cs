using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using ContractManager.Domain.Interfaces;

namespace ContractManager.Application.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<DocumentDto> UploadAsync(UploadDocumentDto dto)
    {
        var document = Document.Create(dto.FileName, dto.FilePath, dto.ContractId);
        var saved = await _documentRepository.AddAsync(document);

        return new DocumentDto
        {
            Id = saved.Id,
            FileName = saved.FileName,
            FilePath = saved.FilePath
        };
    }

    public async Task<IEnumerable<DocumentDto>> GetAllAsync()
    {
        var docs = await _documentRepository.GetAllAsync();
        return docs.Select(d => new DocumentDto
        {
            Id = d.Id,
            FileName = d.FileName,
            FilePath = d.FilePath
        });
    }

    public async Task<DocumentDto?> GetByIdAsync(Guid id)
    {
        var doc = await _documentRepository.GetByIdAsync(id);
        if (doc == null) return null;

        return new DocumentDto
        {
            Id = doc.Id,
            FileName = doc.FileName,
            FilePath = doc.FilePath
        };
    }

    public async Task<IEnumerable<DocumentDto>> GetByContractIdAsync(Guid contractId)
    {
        var docs = await _documentRepository.GetByContractIdAsync(contractId);
        return docs.Select(d => new DocumentDto
        {
            Id = d.Id,
            FileName = d.FileName,
            FilePath = d.FilePath
        });
    }
}
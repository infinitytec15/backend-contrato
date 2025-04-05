namespace ContractManager.Application.DTOs;

public class UploadDocumentDto
{
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public Guid ContractId { get; set; }
}
using Swashbuckle.AspNetCore.Annotations;

namespace ContractManager.Application.DTOs;

public class UploadDocumentDto
{
    public string FileName { get; set; } = null!;
    public string FileBase64 { get; set; } = null!;
    public Guid ContractId { get; set; }
}

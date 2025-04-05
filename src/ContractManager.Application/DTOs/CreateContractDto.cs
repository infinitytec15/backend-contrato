namespace ContractManager.Application.DTOs;

public class CreateContractDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
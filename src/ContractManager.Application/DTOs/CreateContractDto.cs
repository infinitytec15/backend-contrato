namespace ContractManager.Application.DTOs;

public class CreateContractDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid ClientId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>(); // <-- array, será convertido no Service
}
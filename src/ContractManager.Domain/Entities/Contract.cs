namespace ContractManager.Domain.Entities;

public class Contract
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get; set; }
    public List<string>? Tags { get; set; }
}
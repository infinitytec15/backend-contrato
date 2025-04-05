namespace ContractManager.Domain.Entities;

public class Alert
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
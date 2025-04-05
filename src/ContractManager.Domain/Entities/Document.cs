namespace ContractManager.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
}
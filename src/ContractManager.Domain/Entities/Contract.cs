using ContractManager.Domain.Enums;

namespace ContractManager.Domain.Entities;

public class Contract
{
    // Construtor principal com validações básicas
    public Contract(string title, string content, Guid clientId, DateTime? expirationDate = null)
    {
        Id = Guid.NewGuid();
        Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentException("Título é obrigatório") : title;
        Content = string.IsNullOrWhiteSpace(content) ? throw new ArgumentException("Conteúdo é obrigatório") : content;
        ClientId = clientId == Guid.Empty ? throw new ArgumentException("ClientId inválido") : clientId;
        ExpirationDate = expirationDate;
        CreatedAt = DateTime.UtcNow;
        Status = ContractStatus.Draft;
        Tags = new List<string>();
    }

    // Construtor protegido para uso do EF Core
    protected Contract() { }

    // Propriedades
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public Guid ClientId { get; private set; }
    public ContractStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public List<string> Tags { get; private set; }

    // Métodos de domínio
    public void AddTag(string tag)
    {
        if (!string.IsNullOrWhiteSpace(tag) && !Tags.Contains(tag))
            Tags.Add(tag);
    }

    public void UpdateStatus(ContractStatus newStatus)
    {
        Status = newStatus;
    }

    public void UpdateContent(string title, string content, DateTime? expirationDate = null)
    {
        if (!string.IsNullOrWhiteSpace(title)) Title = title;
        if (!string.IsNullOrWhiteSpace(content)) Content = content;
        ExpirationDate = expirationDate;
    }
}
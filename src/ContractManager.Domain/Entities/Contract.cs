using ContractManager.Domain.Enums;
using ContractManager.Domain.Entities; // Garante que o ContractFormResponse está visível

namespace ContractManager.Domain.Entities;

public class Contract
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public Guid ClientId { get; private set; }
    public ContractStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    private readonly List<string> _tags = new();
    public IReadOnlyList<string> Tags => _tags;

    private readonly List<ContractFormResponse> _formResponses = new();
    public IReadOnlyCollection<ContractFormResponse> FormResponses => _formResponses.AsReadOnly();

    private Contract() { }

    public static Contract Create(string title, string content, Guid clientId, DateTime? expirationDate, List<string> tags)
    {
        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            Title = title,
            Content = content,
            ClientId = clientId,
            Status = ContractStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            ExpirationDate = expirationDate
        };

        if (tags is not null && tags.Any())
            contract._tags.AddRange(tags);

        return contract;
    }

    public void Approve() => Status = ContractStatus.Approved;
    public void Reject() => Status = ContractStatus.Rejected;

    public void AttachFormResponse(ContractFormResponse response)
    {
        if (response is not null)
            _formResponses.Add(response);
    }
}

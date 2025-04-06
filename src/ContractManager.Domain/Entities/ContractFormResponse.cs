namespace ContractManager.Domain.Entities;

public class ContractFormResponse
{
    public Guid Id { get; private set; }
    public Guid ContractId { get; private set; }
    public string FieldKey { get; private set; } = null!;
    public string FieldValue { get; private set; } = null!;
    public DateTime SubmittedAt { get; private set; }

    public ContractFormResponse(Guid contractId, string fieldKey, string fieldValue)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        FieldKey = fieldKey;
        FieldValue = fieldValue;
        SubmittedAt = DateTime.UtcNow;
    }

    // EF Core
    private ContractFormResponse() { }
}

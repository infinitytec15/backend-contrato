namespace ContractManager.Domain.Entities;

public class ContractFormField
{
    public Guid Id { get; private set; }
    public Guid ContractId { get; private set; }
    public string FieldName { get; private set; } = null!;
    public string FieldType { get; private set; } = null!;
    public bool IsRequired { get; private set; }

    // 🔧 Construtor para uso do EF Core
    private ContractFormField() { }

    // ✅ Construtor usado pela aplicação
    public ContractFormField(Guid contractId, string fieldName, string fieldType, bool isRequired)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        FieldName = fieldName;
        FieldType = fieldType;
        IsRequired = isRequired;
    }
}
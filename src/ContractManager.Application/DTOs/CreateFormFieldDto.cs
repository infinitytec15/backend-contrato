namespace ContractManager.Application.DTOs;

public class CreateFormFieldDto
{
    public Guid ContractId { get; set; }
    public string FieldName { get; set; } = null!;
    public string FieldType { get; set; } = null!;
    public bool IsRequired { get; set; }
}
namespace ContractManager.Application.DTOs;

public class CreateContractFormDto
{
    public Guid ContractId { get; set; }
    public List<FormFieldDto> Fields { get; set; } = new();
}

public class FormFieldDto
{
    public string Label { get; set; } = null!;
    public string Type { get; set; } = null!; // ex: "text", "email", "cpf"
    public bool Required { get; set; } = true;
}
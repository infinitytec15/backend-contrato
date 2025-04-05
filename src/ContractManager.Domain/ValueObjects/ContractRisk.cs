namespace ContractManager.Domain.ValueObjects;

public class ContractRisk
{
    public int Score { get; set; }  // de 0 a 100
    public string Summary { get; set; } = string.Empty;
}
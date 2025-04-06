namespace ContractManager.Application.Interfaces;

public interface IContractAiService
{
    Task<string> AnalisarContratoAsync(string contrato);
    Task<string> GerarContratoAsync(string prompt);
}
using System;
using System.Threading.Tasks;

namespace ContractManager.Application.Interfaces;

/// <summary>
/// Serviço para integração com a Zapsign (assinatura digital de contratos).
/// </summary>
public interface IZapsignService
{
    /// <summary>
    /// Envia um contrato para assinatura via Zapsign.
    /// </summary>
    /// <param name="contractId">ID do contrato que será enviado.</param>
    /// <param name="signatoryEmail">E-mail do signatário (quem irá assinar).</param>
    /// <param name="contractContent">Conteúdo do contrato em texto plano (será convertido para PDF).</param>
    /// <returns>Resposta JSON da Zapsign contendo status e ID da assinatura.</returns>
    Task<string> EnviarParaAssinaturaAsync(Guid contractId, string signatoryEmail, string contractContent);
}
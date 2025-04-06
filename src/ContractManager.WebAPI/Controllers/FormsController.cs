using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormsController : ControllerBase
{
    private readonly IContractFormService _formService;

    public FormsController(IContractFormService formService)
    {
        _formService = formService;
    }

    /// <summary>
    /// Envia as respostas preenchidas de um formulário de contrato.
    /// </summary>
    /// <param name="dto">DTO contendo os campos e valores preenchidos.</param>
    /// <returns>Status 200 se for salvo com sucesso.</returns>
    [HttpPost("submit")]
    [AllowAnonymous]
    public async Task<IActionResult> Submit([FromBody] SubmitContractFormDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _formService.SaveFormResponsesAsync(dto);
        return Ok(new { message = "Respostas recebidas com sucesso!" });
    }

    /// <summary>
    /// Gera o conteúdo final do contrato substituindo os campos preenchidos.
    /// </summary>
    /// <param name="contractId">ID do contrato.</param>
    /// <returns>Contrato final com os dados preenchidos.</returns>
    [HttpGet("{contractId}/generate")]
    [AllowAnonymous]
    public async Task<IActionResult> Generate(Guid contractId)
    {
        var content = await _formService.GenerateContractWithResponsesAsync(contractId);
        return Ok(new { content });
    }
}

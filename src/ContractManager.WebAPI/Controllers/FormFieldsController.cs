using Microsoft.AspNetCore.Mvc;
using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/contracts/{contractId:guid}/fields")]
public class FormFieldsController : ControllerBase
{
    private readonly IContractFormFieldService _formFieldService;

    public FormFieldsController(IContractFormFieldService formFieldService)
    {
        _formFieldService = formFieldService;
    }

    /// <summary>
    /// Adiciona um novo campo ao contrato
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddField([FromRoute] Guid contractId, [FromBody] CreateFormFieldDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        dto.ContractId = contractId;
        await _formFieldService.AddFieldAsync(dto);

        return Ok(new { message = "Campo adicionado com sucesso!" });
    }

    /// <summary>
    /// Lista todos os campos de formulário vinculados ao contrato
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetFields([FromRoute] Guid contractId)
    {
        var fields = await _formFieldService.GetFieldsAsync(contractId);
        return Ok(fields);
    }
}
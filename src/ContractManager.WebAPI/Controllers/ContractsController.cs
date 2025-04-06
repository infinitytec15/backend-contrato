using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly IZapsignService _zapsignService;
    private readonly IContractFormService _contractFormService;

    public ContractsController(
        IContractService contractService,
        IZapsignService zapsignService,
        IContractFormService contractFormService)
    {
        _contractService = contractService;
        _zapsignService = zapsignService;
        _contractFormService = contractFormService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _contractService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contracts = await _contractService.GetAllAsync();
        return Ok(contracts);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contract = await _contractService.GetByIdAsync(id);
        if (contract == null)
            return NotFound();

        return Ok(contract);
    }

    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> Approve(Guid id)
    {
        var result = await _contractService.ApproveAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id)
    {
        var result = await _contractService.RejectAsync(id);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/send-signature")]
    public async Task<IActionResult> SendToSign(Guid id, [FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("E-mail é obrigatório para envio.");

        var contractContent = await _contractFormService.GenerateContractWithResponsesAsync(id);
        var result = await _zapsignService.EnviarParaAssinaturaAsync(id, email, contractContent);

        return Ok(new { link = result });
    }
}
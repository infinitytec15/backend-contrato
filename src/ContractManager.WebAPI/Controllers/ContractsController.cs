using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using ContractManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _contractService.CreateContractAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var contract = await _contractService.GetContractByIdAsync(id);
        if (contract == null)
            return NotFound();

        return Ok(contract);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contracts = await _contractService.GetAllContractsAsync();
        return Ok(contracts);
    }
}
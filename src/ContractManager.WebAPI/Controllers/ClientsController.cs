using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // 🔒 Exige autenticação JWT para todas as rotas
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdClient = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    /// <summary>
    /// Retorna todos os clientes.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    /// <summary>
    /// Retorna um cliente específico por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        return client == null ? NotFound() : Ok(client);
    }
}
using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    // POST: /api/clients
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdClient = await _clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    // GET: /api/clients
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    // GET: /api/clients/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }
}
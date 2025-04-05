using ContractManager.Application.DTOs;
using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    // POST: /api/documents
    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] UploadDocumentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _documentService.UploadAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // GET: /api/documents
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var documents = await _documentService.GetAllAsync();
        return Ok(documents);
    }

    // GET: /api/documents/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var document = await _documentService.GetByIdAsync(id);
        if (document == null)
            return NotFound();

        return Ok(document);
    }

    // GET: /api/documents/contract/{contractId}
    [HttpGet("contract/{contractId:guid}")]
    public async Task<IActionResult> GetByContractId(Guid contractId)
    {
        var documents = await _documentService.GetByContractIdAsync(contractId);
        return Ok(documents);
    }
}
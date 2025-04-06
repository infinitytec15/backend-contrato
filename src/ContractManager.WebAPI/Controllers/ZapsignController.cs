using ContractManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ZapsignController : ControllerBase
{
    private readonly IZapsignService _zapsign;

    public ZapsignController(IZapsignService zapsign)
    {
        _zapsign = zapsign;
    }

    [HttpPost("enviar/{contractId}")]
    public async Task<IActionResult> Enviar(Guid contractId, [FromQuery] string email, [FromBody] string contratoHtml)
    {
        var result = await _zapsign.EnviarParaAssinaturaAsync(contractId, email, contratoHtml);
        return Ok(result);
    }
}
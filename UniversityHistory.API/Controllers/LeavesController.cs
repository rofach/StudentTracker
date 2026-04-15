using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LeavesController : ControllerBase
{
    private readonly IMovementService _movementService;

    public LeavesController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpPut("{id:guid}/close")]
    public async Task<IActionResult> Close(Guid id, [FromBody] CloseAcademicLeaveDto dto, CancellationToken ct)
    {
        var result = await _movementService.CloseLeaveAsync(id, dto, ct);
        return Ok(result);
    }
}


using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReportsController : ControllerBase
{
    private readonly IMovementService _movementService;

    public ReportsController(IMovementService movementService)
    {
        _movementService = movementService;
    }

    [HttpGet("academic-difference/active")]
    public async Task<IActionResult> GetActiveAcademicDifference(
        CancellationToken ct,
        [FromQuery] string? studentName,
        [FromQuery] string? disciplineName,
        [FromQuery] string? status,
        [FromQuery] DateOnly? dateFrom,
        [FromQuery] DateOnly? dateTo,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _movementService.GetActiveAcademicDifferenceAsync(
            studentName, disciplineName, status, dateFrom, dateTo, page, pageSize, ct));
    }

    [HttpGet("internal-transfers")]
    public async Task<IActionResult> GetInternalTransfers(
        CancellationToken ct,
        [FromQuery] string? studentName,
        [FromQuery] DateOnly? dateFrom,
        [FromQuery] DateOnly? dateTo,
        [FromQuery] bool onlyWithPendingDifference = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _movementService.GetInternalTransferJournalAsync(
            studentName, dateFrom, dateTo, onlyWithPendingDifference, page, pageSize, ct));
    }
}

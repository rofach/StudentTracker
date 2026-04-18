using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;
    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] EnrollStudentDto dto, CancellationToken ct)
    {
        var enrollmentId = await _enrollmentService.EnrollStudentAsync(dto, ct);
        return CreatedAtAction(nameof(Close), new { id = enrollmentId }, new { enrollmentId });
    }

    [HttpPut("{id:guid}/close")]
    public async Task<IActionResult> Close(Guid id, [FromBody] CloseEnrollmentDto dto, CancellationToken ct)
    {
        await _enrollmentService.CloseEnrollmentAsync(id, dto, ct);
        return NoContent();
    }

    [HttpPut("{id:guid}/subgroup")]
    public async Task<IActionResult> AssignSubgroup(Guid id, [FromBody] AssignSubgroupDto dto, CancellationToken ct)
    {
        await _enrollmentService.AssignSubgroupAsync(id, dto, ct);
        return NoContent();
    }

    [HttpPost("{id:guid}/subgroup-move")]
    public async Task<IActionResult> MoveSubgroup(Guid id, [FromBody] MoveStudentToSubgroupDto dto, CancellationToken ct)
    {
        await _enrollmentService.MoveSubgroupAsync(id, dto, ct);
        return NoContent();
    }
}



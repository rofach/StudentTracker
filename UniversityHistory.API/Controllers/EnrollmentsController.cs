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

    [HttpPut("{id:int}/close")]
    public async Task<IActionResult> Close(int id, [FromBody] CloseEnrollmentDto dto, CancellationToken ct)
    {
        await _enrollmentService.CloseEnrollmentAsync(id, dto, ct);
        return NoContent();
    }
}

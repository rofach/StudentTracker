using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AcademicUnitsController : ControllerBase
{
    private readonly IAcademicUnitService _service;
    public AcademicUnitsController(IAcademicUnitService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAcademicUnitDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.AcademicUnitId }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAcademicUnitDto dto, CancellationToken ct) =>
        Ok(await _service.UpdateAsync(id, dto, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}


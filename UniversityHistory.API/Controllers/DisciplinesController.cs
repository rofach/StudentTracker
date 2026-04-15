using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DisciplinesController : ControllerBase
{
    private readonly IDisciplineService _service;
    public DisciplinesController(IDisciplineService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDisciplineDto dto, CancellationToken ct)
    {
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.DisciplineId }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDisciplineDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeleteAsync(id, ct);
        return NoContent();
    }
}


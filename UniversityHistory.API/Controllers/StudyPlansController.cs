using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class StudyPlansController : ControllerBase
{
    private readonly IStudyPlanService _service;
    public StudyPlansController(IStudyPlanService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllPlansAsync(ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _service.GetPlanByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudyPlanDto dto, CancellationToken ct)
    {
        var created = await _service.CreatePlanAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.PlanId }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudyPlanDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdatePlanAsync(id, dto, ct);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _service.DeletePlanAsync(id, ct);
        return NoContent();
    }

    [HttpGet("{id:guid}/disciplines")]
    public async Task<IActionResult> GetDisciplines(Guid id, CancellationToken ct)
    {
        return Ok(await _service.GetPlanDisciplinesAsync(id, ct));
    }

    [HttpPost("{id:guid}/disciplines")]
    public async Task<IActionResult> AddDiscipline(Guid id, [FromBody] AddPlanDisciplineDto dto, CancellationToken ct)
    {
        var result = await _service.AddPlanDisciplineAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetDisciplines), new { id }, result);
    }

    [HttpPut("{id:guid}/disciplines/{disciplineId:int}")]
    public async Task<IActionResult> UpdateDiscipline(Guid id, Guid disciplineId, [FromBody] UpdatePlanDisciplineDto dto, CancellationToken ct)
    {
        var result = await _service.UpdatePlanDisciplineAsync(id, disciplineId, dto, ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}/disciplines/{disciplineId:int}")]
    public async Task<IActionResult> DeleteDiscipline(Guid id, Guid disciplineId, CancellationToken ct)
    {
        await _service.DeletePlanDisciplineAsync(id, disciplineId, ct);
        return NoContent();
    }
}


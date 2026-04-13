using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class GroupsController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly IStudyPlanService _planService;

    public GroupsController(IGroupService groupService, IStudyPlanService planService)
    {
        _groupService = groupService;
        _planService = planService;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveGroups([FromQuery] DateOnly? date, CancellationToken ct) =>
        Ok(await _groupService.GetActiveGroupsAsync(date, ct));

    [HttpGet("{id:int}/composition")]
    public async Task<IActionResult> GetComposition(
        int id, [FromQuery] DateOnly? date, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetCompositionAsync(id, date, page, pageSize, ct));
    }

    [HttpGet("{id:int}/students")]
    public async Task<IActionResult> GetStudents(
        int id, [FromQuery] DateOnly? date, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetStudentsInGroupAsync(id, date, page, pageSize, ct));
    }

    // ── Group plan assignments ────────────────────────────────────────────────

    [HttpGet("{id:int}/plans")]
    public async Task<IActionResult> GetPlanHistory(int id, CancellationToken ct) =>
        Ok(await _planService.GetGroupPlanHistoryAsync(id, ct));

    [HttpPost("{id:int}/plans")]
    public async Task<IActionResult> AssignPlan(int id, [FromBody] AssignGroupPlanDto dto, CancellationToken ct)
    {
        var result = await _planService.AssignGroupPlanAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetPlanHistory), new { id }, result);
    }

    [HttpPut("{id:int}/plans/current")]
    public async Task<IActionResult> ChangePlan(int id, [FromBody] ChangeGroupPlanDto dto, CancellationToken ct) =>
        Ok(await _planService.ChangeGroupPlanAsync(id, dto, ct));
}

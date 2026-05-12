using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Authorize(Roles = AuthRoles.Admin)]
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

    [HttpGet("{id:guid}/composition")]
    public async Task<IActionResult> GetComposition(
        Guid id, [FromQuery] DateOnly? date, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetCompositionAsync(id, date, page, pageSize, ct));
    }

    [HttpGet("{id:guid}/students")]
    public async Task<IActionResult> GetStudents(
        Guid id, [FromQuery] DateOnly? date, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetStudentsInGroupAsync(id, date, page, pageSize, ct));
    }

    [HttpGet("{id:guid}/subgroups")]
    public async Task<IActionResult> GetSubgroups(Guid id, CancellationToken ct) =>
        Ok(await _groupService.GetSubgroupsAsync(id, ct));

    [HttpGet("{id:guid}/plans")]
    public async Task<IActionResult> GetPlanHistory(Guid id, CancellationToken ct) =>
        Ok(await _planService.GetGroupPlanHistoryAsync(id, ct));

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto dto, CancellationToken ct)
    {
        var result = await _groupService.CreateGroupAsync(dto, ct);
        return CreatedAtAction(nameof(GetActiveGroups), result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] UpdateGroupDto dto, CancellationToken ct) =>
        Ok(await _groupService.UpdateGroupAsync(id, dto, ct));

    [HttpPost("{id:guid}/plans")]
    public async Task<IActionResult> AssignPlan(Guid id, [FromBody] AssignGroupPlanDto dto, CancellationToken ct)
    {
        var result = await _planService.AssignGroupPlanAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetPlanHistory), new { id }, result);
    }

    [HttpPut("{id:guid}/plans/current")]
    public async Task<IActionResult> ChangePlan(Guid id, [FromBody] ChangeGroupPlanDto dto, CancellationToken ct) =>
        Ok(await _planService.ChangeGroupPlanAsync(id, dto, ct));
}


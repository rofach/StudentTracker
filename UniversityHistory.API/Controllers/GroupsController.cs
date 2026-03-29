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
    public GroupsController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveGroups([FromQuery] DateOnly? date, CancellationToken ct)
    {
        return Ok(await _groupService.GetActiveGroupsAsync(date, ct));
    }

    [HttpGet("{id:int}/composition")]
    public async Task<IActionResult> GetComposition(
        int id, [FromQuery] DateOnly? date, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetCompositionAsync(id, date, page, pageSize, ct));
    }

    [HttpGet("{id:int}/students")]
    public async Task<IActionResult> GetStudents(
        int id, [FromQuery] DateOnly? date, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _groupService.GetStudentsInGroupAsync(id, date, page, pageSize, ct));
    }
}

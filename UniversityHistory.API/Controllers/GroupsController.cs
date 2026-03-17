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

    [HttpGet("{id:int}/composition")]
    public async Task<IActionResult> GetComposition(
        int id, [FromQuery] DateOnly? date, CancellationToken ct)
    {
        return Ok(await _groupService.GetCompositionAsync(id, date, ct));
    }
}

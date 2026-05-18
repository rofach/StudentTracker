using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Authorize(Roles = AuthRoles.Admin)]
[Route("api/[controller]")]
[Produces("application/json")]
public class InstitutionsController : ControllerBase
{
    private readonly IInstitutionService _service;

    public InstitutionsController(IInstitutionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string institutionName, CancellationToken ct)
    {
        return Ok(await _service.CreateAsync(institutionName, ct));
    }
}

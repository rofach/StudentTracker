using Microsoft.AspNetCore.Mvc;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetAllAsync(page, pageSize, ct));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var student = await _studentService.GetByIdAsync(id, ct);
        return student is null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto dto, CancellationToken ct)
    {
        var created = await _studentService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.StudentId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto, CancellationToken ct)
    {
        var updated = await _studentService.UpdateAsync(id, dto, ct);
        return Ok(updated);
    }

    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto, CancellationToken ct)
    {
        await _studentService.ChangeStatusAsync(id, dto, ct);
        return NoContent();
    }

    [HttpGet("{id:int}/details")]
    public async Task<IActionResult> GetDetails(int id, CancellationToken ct)
    {
        return Ok(await _studentService.GetDetailAsync(id, ct));
    }

    [HttpGet("{id:int}/timeline")]
    public async Task<IActionResult> GetTimeline(int id, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetTimelineAsync(id, page, pageSize, ct));
    }

    [HttpGet("{id:int}/classmates")]
    public async Task<IActionResult> GetClassmates(int id, [FromQuery] DateOnly? dateFrom, [FromQuery] DateOnly? dateTo, CancellationToken ct)
    {
        return Ok(await _studentService.GetClassmatesAsync(id, dateFrom, dateTo, ct));
    }

    [HttpGet("{id:int}/group")]
    public async Task<IActionResult> GetGroup(int id, [FromQuery] DateOnly? date, CancellationToken ct)
    {
        var result = await _studentService.GetGroupOnDateAsync(id, date, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:int}/movements")]
    public async Task<IActionResult> GetMovements(int id,
        [FromServices] IMovementService movementService, CancellationToken ct)
    {
        return Ok(await movementService.GetMovementsAsync(id, ct));
    }

    [HttpPost("{id:int}/transfers")]
    public async Task<IActionResult> CreateTransfer(int id, [FromBody] CreateTransferDto dto,
        [FromServices] IMovementService movementService, CancellationToken ct)
    {
        var result = await movementService.CreateTransferAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }

    [HttpPost("{id:int}/leaves")]
    public async Task<IActionResult> CreateLeave(int id, [FromBody] CreateLeaveDto dto,
        [FromServices] IMovementService movementService, CancellationToken ct)
    {
        var result = await movementService.CreateLeaveAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }

    [HttpGet("{id:int}/plans")]
    public async Task<IActionResult> GetPlans(int id,
        [FromServices] IStudyPlanService planService, CancellationToken ct)
    {
        return Ok(await planService.GetPlanAssignmentsAsync(id, ct));
    }

    [HttpPost("{id:int}/plans")]
    public async Task<IActionResult> AssignPlan(int id, [FromBody] AssignPlanDto dto,
        [FromServices] IStudyPlanService planService, CancellationToken ct)
    {
        var result = await planService.AssignPlanAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetPlans), new { id }, result);
    }

    [HttpPost("{id:int}/move")]
    public async Task<IActionResult> MoveToGroup(int id, [FromBody] MoveStudentDto dto,
        [FromServices] IEnrollmentService enrollmentService, CancellationToken ct)
    {
        await enrollmentService.MoveToGroupAsync(id, dto, ct);
        return NoContent();
    }

    [HttpGet("{id:int}/grades")]
    public async Task<IActionResult> GetGrades(int id,
        [FromServices] IGradeService gradeService, CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await gradeService.GetGradesAsync(id, page, pageSize, ct));
    }

    [HttpGet("{id:int}/grades/average")]
    public async Task<IActionResult> GetAverageGrade(
        int id,
        [FromServices] IGradeService gradeService,
        [FromQuery] int? semesterNo,
        [FromQuery] int? disciplineId,
        [FromQuery] int? academicYearStart,
        CancellationToken ct)
    {
        return Ok(await gradeService.GetAverageGradeAsync(id, semesterNo, disciplineId, academicYearStart, ct));
    }
}

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
    private readonly IMovementService _movementService;
    private readonly IGradeService _gradeService;
    private readonly IEnrollmentService _enrollmentService;

    public StudentsController(
        IStudentService studentService,
        IMovementService movementService,
        IGradeService gradeService,
        IEnrollmentService enrollmentService)
    {
        _studentService = studentService;
        _movementService = movementService;
        _gradeService = gradeService;
        _enrollmentService = enrollmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetAllAsync(page, pageSize, ct));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        CancellationToken ct,
        [FromQuery] string? fullName,
        [FromQuery] string? email,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.SearchAsync(fullName, email, status, page, pageSize, ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] StudentUpdateDto dto, CancellationToken ct)
    {
        var updated = await _studentService.UpdateAsync(id, dto, ct);
        return Ok(updated);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusDto dto, CancellationToken ct)
    {
        await _studentService.ChangeStatusAsync(id, dto, ct);
        return NoContent();
    }

    [HttpGet("{id:guid}/details")]
    public async Task<IActionResult> GetDetails(Guid id, CancellationToken ct)
    {
        return Ok(await _studentService.GetDetailAsync(id, ct));
    }

    [HttpGet("{id:guid}/timeline")]
    public async Task<IActionResult> GetTimeline(
        Guid id,
        CancellationToken ct,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetTimelineAsync(id, page, pageSize, ct));
    }

    [HttpGet("{id:guid}/classmates")]
    public async Task<IActionResult> GetClassmates(
        Guid id,
        [FromQuery] DateOnly? dateFrom,
        [FromQuery] DateOnly? dateTo,
        CancellationToken ct)
    {
        var classmates = await _studentService.GetClassmatesAsync(id, dateFrom, dateTo, ct);
        return Ok(classmates);
    }

    [HttpGet("{id:guid}/group")]
    public async Task<IActionResult> GetGroup(Guid id, [FromQuery] DateOnly? date, CancellationToken ct)
    {
        var result = await _studentService.GetGroupOnDateAsync(id, date, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:guid}/movements")]
    public async Task<IActionResult> GetMovements(Guid id, CancellationToken ct)
    {
        return Ok(await _movementService.GetMovementsAsync(id, ct));
    }

    [HttpPost("{id:guid}/transfers")]
    public async Task<IActionResult> CreateTransfer(Guid id, [FromBody] CreateTransferDto dto, CancellationToken ct)
    {
        var result = await _movementService.CreateTransferAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }

    [HttpPost("{id:guid}/leaves")]
    public async Task<IActionResult> CreateLeave(Guid id, [FromBody] CreateLeaveDto dto, CancellationToken ct)
    {
        var result = await _movementService.CreateLeaveAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }


    [HttpPost("{id:guid}/move")]
    public async Task<IActionResult> MoveToGroup(Guid id, [FromBody] MoveStudentDto dto, CancellationToken ct)
    {
        await _enrollmentService.MoveToGroupAsync(id, dto, ct);
        return NoContent();
    }

    [HttpGet("{id:guid}/group-transfers")]
    public async Task<IActionResult> GetGroupTransfers(Guid id, CancellationToken ct)
    {
        var result = await _enrollmentService.GetGroupTransfersAsync(id, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/group-transfers/{transferId:guid}")]
    public async Task<IActionResult> GetGroupTransferDetail(Guid id, Guid transferId, CancellationToken ct)
    {
        var result = await _enrollmentService.GetGroupTransferDetailAsync(id, transferId, ct);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/group-transfers/{transferId:guid}/difference-items/{itemId:guid}")]
    public async Task<IActionResult> UpdateDifferenceItem(
        Guid id, Guid transferId, Guid itemId, [FromBody] UpdateDifferenceItemDto dto, CancellationToken ct)
    {
        var result = await _enrollmentService.UpdateDifferenceItemAsync(id, transferId, itemId, dto, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/grades")]
    public async Task<IActionResult> GetGrades(Guid id, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _gradeService.GetGradesAsync(id, page, pageSize, ct));
    }

    [HttpGet("{id:guid}/disciplines")]
    public async Task<IActionResult> GetDisciplines(Guid id, CancellationToken ct)
    {
        return Ok(await _gradeService.GetStudentDisciplinesAsync(id, ct));
    }

    [HttpGet("{id:guid}/grades/average")]
    public async Task<IActionResult> GetAverageGrade(Guid id,
        [FromQuery] int? semesterNo, [FromQuery] Guid? disciplineId,
        [FromQuery] int? academicYearStart, CancellationToken ct)
    {
        return Ok(await _gradeService.GetAverageGradeAsync(id, semesterNo, disciplineId, academicYearStart, ct));
    }

    [HttpPut("{id:guid}/grades/{courseEnrollmentId:guid}")]
    public async Task<IActionResult> UpsertGrade(
        Guid id,
        Guid courseEnrollmentId,
        [FromBody] UpsertGradeDto dto,
        CancellationToken ct)
    {
        var result = await _gradeService.UpsertGradeAsync(id, courseEnrollmentId, dto, ct);
        return Ok(result);
    }
}


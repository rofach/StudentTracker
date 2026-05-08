using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityHistory.API.Auth;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;

namespace UniversityHistory.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IStudentAccountService _studentAccountService;
    private readonly IMovementService _movementService;
    private readonly IGradeService _gradeService;
    private readonly IEnrollmentService _enrollmentService;

    public StudentsController(
        IStudentService studentService,
        IStudentAccountService studentAccountService,
        IMovementService movementService,
        IGradeService gradeService,
        IEnrollmentService enrollmentService)
    {
        _studentService = studentService;
        _studentAccountService = studentAccountService;
        _movementService = movementService;
        _gradeService = gradeService;
        _enrollmentService = enrollmentService;
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetAllAsync(page, pageSize, ct));
    }

    [Authorize(Roles = AuthRoles.Admin)]
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

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var student = await _studentService.GetByIdAsync(id, ct);
        return student is null ? NotFound() : Ok(student);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudentCreateDto dto, CancellationToken ct)
    {
        var created = await _studentAccountService.CreateStudentAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Student.StudentId }, created);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] StudentUpdateDto dto, CancellationToken ct)
    {
        var updated = await _studentAccountService.UpdateStudentAsync(id, dto, ct);
        return Ok(updated);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("{id:guid}/account/reset-password")]
    public async Task<IActionResult> ResetPassword(Guid id, [FromBody] ResetStudentPasswordDto dto, CancellationToken ct)
    {
        var result = await _studentAccountService.ResetPasswordAsync(id, dto, ct);
        return Ok(result);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid id, [FromBody] ChangeStatusDto dto, CancellationToken ct)
    {
        await _studentService.ChangeStatusAsync(id, dto, ct);
        return NoContent();
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/details")]
    public async Task<IActionResult> GetDetails(Guid id, CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        return Ok(await _studentService.GetDetailAsync(id, ct));
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/timeline")]
    public async Task<IActionResult> GetTimeline(
        Guid id,
        CancellationToken ct,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        page = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _studentService.GetTimelineAsync(id, page, pageSize, ct));
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/classmates")]
    public async Task<IActionResult> GetClassmates(
        Guid id,
        [FromQuery] DateOnly? dateFrom,
        [FromQuery] DateOnly? dateTo,
        CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        var classmates = await _studentService.GetClassmatesAsync(id, dateFrom, dateTo, ct);
        return Ok(classmates);
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/group")]
    public async Task<IActionResult> GetGroup(Guid id, [FromQuery] DateOnly? date, CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        var result = await _studentService.GetGroupOnDateAsync(id, date, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/movements")]
    public async Task<IActionResult> GetMovements(Guid id, CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        return Ok(await _movementService.GetMovementsAsync(id, ct));
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("{id:guid}/transfers")]
    public async Task<IActionResult> CreateTransfer(Guid id, [FromBody] CreateTransferDto dto, CancellationToken ct)
    {
        var result = await _movementService.CreateTransferAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("{id:guid}/leaves")]
    public async Task<IActionResult> CreateLeave(Guid id, [FromBody] CreateLeaveDto dto, CancellationToken ct)
    {
        var result = await _movementService.CreateLeaveAsync(id, dto, ct);
        return CreatedAtAction(nameof(GetMovements), new { id }, result);
    }


    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPost("{id:guid}/move")]
    public async Task<IActionResult> MoveToGroup(Guid id, [FromBody] MoveStudentDto dto, CancellationToken ct)
    {
        await _enrollmentService.MoveToGroupAsync(id, dto, ct);
        return NoContent();
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpGet("{id:guid}/group-transfers")]
    public async Task<IActionResult> GetGroupTransfers(Guid id, CancellationToken ct)
    {
        var result = await _enrollmentService.GetGroupTransfersAsync(id, ct);
        return Ok(result);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpGet("{id:guid}/group-transfers/{transferId:guid}")]
    public async Task<IActionResult> GetGroupTransferDetail(Guid id, Guid transferId, CancellationToken ct)
    {
        var result = await _enrollmentService.GetGroupTransferDetailAsync(id, transferId, ct);
        return Ok(result);
    }

    [Authorize(Roles = AuthRoles.Admin)]
    [HttpPatch("{id:guid}/group-transfers/{transferId:guid}/difference-items/{itemId:guid}")]
    public async Task<IActionResult> UpdateDifferenceItem(
        Guid id, Guid transferId, Guid itemId, [FromBody] UpdateDifferenceItemDto dto, CancellationToken ct)
    {
        var result = await _enrollmentService.UpdateDifferenceItemAsync(id, transferId, itemId, dto, ct);
        return Ok(result);
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/grades")]
    public async Task<IActionResult> GetGrades(Guid id, CancellationToken ct,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        page     = Math.Max(1, page);
        pageSize = Math.Min(100, Math.Max(1, pageSize));
        return Ok(await _gradeService.GetGradesAsync(id, page, pageSize, ct));
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/disciplines")]
    public async Task<IActionResult> GetDisciplines(Guid id, CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        return Ok(await _gradeService.GetStudentDisciplinesAsync(id, ct));
    }

    [Authorize(Roles = $"{AuthRoles.Admin},{AuthRoles.Student}")]
    [HttpGet("{id:guid}/grades/average")]
    public async Task<IActionResult> GetAverageGrade(Guid id,
        [FromQuery] int? semesterNo, [FromQuery] Guid? disciplineId,
        [FromQuery] int? academicYearStart, CancellationToken ct)
    {
        var accessResult = EnsureStudentAccess(id);
        if (accessResult is not null)
            return accessResult;

        return Ok(await _gradeService.GetAverageGradeAsync(id, semesterNo, disciplineId, academicYearStart, ct));
    }

    [Authorize(Roles = AuthRoles.Admin)]
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

    private IActionResult? EnsureStudentAccess(Guid requestedStudentId)
    {
        if (User.IsInRole(AuthRoles.Admin))
            return null;

        if (!User.IsInRole(AuthRoles.Student))
            return Forbid();

        var studentId = User.GetStudentId();
        if (!studentId.HasValue || studentId.Value != requestedStudentId)
            return Forbid();

        return null;
    }
}


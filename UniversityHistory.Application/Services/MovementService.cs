using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetActiveAcademicDifference;
using UniversityHistory.Application.Queries.GetInternalTransferJournal;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class MovementService : IMovementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGetActiveAcademicDifferenceQueryHandler _activeAcademicDifferenceHandler;
    private readonly IGetInternalTransferJournalQueryHandler _internalTransferJournalHandler;

    public MovementService(
        IUnitOfWork unitOfWork,
        IGetActiveAcademicDifferenceQueryHandler activeAcademicDifferenceHandler,
        IGetInternalTransferJournalQueryHandler internalTransferJournalHandler)
    {
        _unitOfWork = unitOfWork;
        _activeAcademicDifferenceHandler = activeAcademicDifferenceHandler;
        _internalTransferJournalHandler = internalTransferJournalHandler;
    }

    public async Task<StudentMovementDto> GetMovementsAsync(Guid studentId, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var leaves = await _unitOfWork.AcademicLeaves.GetByStudentIdAsync(studentId, ct);
        var externalTransfers = await _unitOfWork.ExternalTransfers.GetByStudentIdAsync(studentId, ct);
        var internalTransfers = await _unitOfWork.GroupTransfers.GetByStudentIdAsync(studentId, ct);

        return leaves.ToDto(externalTransfers, internalTransfers);
    }

    public Task<PagedResult<ActiveAcademicDifferenceDto>> GetActiveAcademicDifferenceAsync(
        string? studentName,
        string? disciplineName,
        string? status,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _activeAcademicDifferenceHandler.HandleAsync(
            new GetActiveAcademicDifferenceQuery(studentName, disciplineName, status, dateFrom, dateTo, page, pageSize),
            ct);
    }

    public Task<PagedResult<InternalTransferJournalItemDto>> GetInternalTransferJournalAsync(
        string? studentName,
        DateOnly? dateFrom,
        DateOnly? dateTo,
        bool onlyWithPendingDifference = false,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _internalTransferJournalHandler.HandleAsync(
            new GetInternalTransferJournalQuery(studentName, dateFrom, dateTo, onlyWithPendingDifference, page, pageSize),
            ct);
    }

    public async Task<ExternalTransferDto> CreateTransferAsync(Guid studentId, CreateTransferDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var institution = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, ct)
            ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

        var transferType = Enum.Parse<TransferType>(dto.TransferType, ignoreCase: true);
        var transfer = dto.ToEntity(studentId, transferType);
        var created = _unitOfWork.ExternalTransfers.Add(transfer);
        await _unitOfWork.SaveChangesAsync(ct);
        return created.ToDto(institution.InstitutionName);
    }

    public async Task<AcademicLeaveDto> CreateLeaveAsync(Guid studentId, CreateLeaveDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(dto.EnrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), dto.EnrollmentId);

        if (enrollment.StudentId != studentId)
            throw new DomainException($"Enrollment {dto.EnrollmentId} does not belong to student {studentId}.");

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {dto.EnrollmentId} is already closed. Cannot add academic leave.");

        if (dto.StartDate < enrollment.DateFrom)
            throw new DomainException("StartDate cannot be before the enrollment's DateFrom.");

        if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            throw new DomainException("EndDate cannot be before StartDate.");

        var openLeave = await _unitOfWork.AcademicLeaves.GetOpenByEnrollmentIdAsync(dto.EnrollmentId, ct);
        if (openLeave is not null)
            throw new DomainException($"Enrollment {dto.EnrollmentId} already has an open academic leave (leave #{openLeave.LeaveId}).");

        if (await _unitOfWork.AcademicLeaves.HasOverlapAsync(dto.EnrollmentId, dto.StartDate, dto.EndDate, ct: ct))
            throw new DomainException($"Academic leave for enrollment {dto.EnrollmentId} overlaps with an existing leave period.");

        var leave = dto.ToEntity();
        var created = _unitOfWork.AcademicLeaves.Add(leave);

        var today = DateOnly.FromDateTime(DateTime.Today);
        if (dto.StartDate <= today && (!dto.EndDate.HasValue || dto.EndDate.Value >= today))
        {
            enrollment.Student.Status = StudentStatus.OnLeave;
            _unitOfWork.Students.Update(enrollment.Student);
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return created.ToDto();
    }

    public async Task<AcademicLeaveDto> CloseLeaveAsync(Guid leaveId, CloseAcademicLeaveDto dto, CancellationToken ct = default)
    {
        var leave = await _unitOfWork.AcademicLeaves.GetByIdAsync(leaveId, ct)
            ?? throw new NotFoundException(nameof(AcademicLeave), leaveId);

        if (leave.EndDate.HasValue)
            throw new DomainException($"Leave {leaveId} is already closed.");

        if (dto.EndDate < leave.StartDate)
            throw new DomainException("EndDate cannot be before the leave's StartDate.");

        leave.EndDate = dto.EndDate;
        leave.ReturnReason = dto.ReturnReason;
        _unitOfWork.AcademicLeaves.Update(leave);

        var today = DateOnly.FromDateTime(DateTime.Today);
        if (dto.EndDate <= today)
        {
            var hasAnotherActiveLeave = (await _unitOfWork.AcademicLeaves
                    .GetByStudentIdAsync(leave.Enrollment.StudentId, ct))
                .Any(existing =>
                    existing.LeaveId != leave.LeaveId &&
                    existing.StartDate <= today &&
                    (!existing.EndDate.HasValue || existing.EndDate.Value >= today));

            if (!hasAnotherActiveLeave && leave.Enrollment.Student.Status == StudentStatus.OnLeave)
            {
                leave.Enrollment.Student.Status = StudentStatus.Active;
                _unitOfWork.Students.Update(leave.Enrollment.Student);
            }
        }

        await _unitOfWork.SaveChangesAsync(ct);
        return leave.ToDto();
    }
}


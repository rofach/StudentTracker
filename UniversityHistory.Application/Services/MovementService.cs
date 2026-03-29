using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class MovementService : IMovementService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IAcademicLeaveRepository _leaveRepo;
    private readonly IExternalTransferRepository _transferRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;

    public MovementService(
        IStudentRepository studentRepo,
        IAcademicLeaveRepository leaveRepo,
        IExternalTransferRepository transferRepo,
        IEnrollmentRepository enrollmentRepo)
    {
        _studentRepo  = studentRepo;
        _leaveRepo    = leaveRepo;
        _transferRepo = transferRepo;
        _enrollmentRepo = enrollmentRepo;
    }

    public async Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var leaves    = await _leaveRepo.GetByStudentIdAsync(studentId, ct);
        var transfers = await _transferRepo.GetByStudentIdAsync(studentId, ct);

        return new StudentMovementDto(
            Leaves:    leaves.Select(l => new AcademicLeaveDto(l.LeaveId, l.StartDate, l.EndDate, l.Reason)),
            Transfers: transfers.Select(t => new ExternalTransferDto(
                t.TransferId, t.TransferType.ToString(), t.TransferDate, t.Institution.InstitutionName, t.Notes))
        );
    }

    public async Task<ExternalTransferDto> CreateTransferAsync(int studentId, CreateTransferDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var institution = await _transferRepo.GetInstitutionByIdAsync(dto.InstitutionId, ct)
            ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

        if (!Enum.TryParse<TransferType>(dto.TransferType, ignoreCase: true, out var transferType))
            throw new DomainException($"Unknown transfer type '{dto.TransferType}'. Valid values: In, Out.");

        var transfer = new ExternalTransfer
        {
            StudentId     = studentId,
            InstitutionId = dto.InstitutionId,
            TransferType  = transferType,
            TransferDate  = dto.TransferDate,
            Notes         = dto.Notes
        };

        var created = await _transferRepo.AddAsync(transfer, ct);
        return new ExternalTransferDto(
            created.TransferId, created.TransferType.ToString(),
            created.TransferDate, institution.InstitutionName, created.Notes);
    }

    public async Task<AcademicLeaveDto> CreateLeaveAsync(int studentId, CreateLeaveDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollment = await _enrollmentRepo.GetByIdAsync(dto.EnrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), dto.EnrollmentId);

        if (enrollment.StudentId != studentId)
            throw new DomainException($"Enrollment {dto.EnrollmentId} does not belong to student {studentId}.");

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {dto.EnrollmentId} is already closed. Cannot add academic leave.");

        var openLeave = await _leaveRepo.GetOpenByEnrollmentIdAsync(dto.EnrollmentId, ct);
        if (openLeave is not null)
            throw new DomainException($"Enrollment {dto.EnrollmentId} already has an open academic leave (leave #{openLeave.LeaveId}).");

        var leave = new AcademicLeave
        {
            EnrollmentId = dto.EnrollmentId,
            StartDate    = dto.StartDate,
            Reason       = dto.Reason
        };

        var created = await _leaveRepo.AddAsync(leave, ct);
        return new AcademicLeaveDto(created.LeaveId, created.StartDate, created.EndDate, created.Reason);
    }
}

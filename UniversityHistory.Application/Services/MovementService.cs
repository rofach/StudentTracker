using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
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
        _studentRepo = studentRepo;
        _leaveRepo = leaveRepo;
        _transferRepo = transferRepo;
        _enrollmentRepo = enrollmentRepo;
    }

    public async Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var leaves = await _leaveRepo.GetByStudentIdAsync(studentId, ct);
        var transfers = await _transferRepo.GetByStudentIdAsync(studentId, ct);

        return leaves.ToDto(transfers);
    }

    public async Task<ExternalTransferDto> CreateTransferAsync(int studentId, CreateTransferDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var institution = await _transferRepo.GetInstitutionByIdAsync(dto.InstitutionId, ct)
            ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

        var transferType = Enum.Parse<TransferType>(dto.TransferType, ignoreCase: true);
        var transfer = dto.ToEntity(studentId, transferType);
        var created = await _transferRepo.AddAsync(transfer, ct);
        return created.ToDto(institution.InstitutionName);
    }

    public async Task<AcademicLeaveDto> CreateLeaveAsync(int studentId, CreateLeaveDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollment = await _enrollmentRepo.GetByIdAsync(dto.EnrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), dto.EnrollmentId);

        if (enrollment.StudentId != studentId)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} does not belong to student {studentId}.");
        }

        if (enrollment.DateTo.HasValue)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} is already closed. Cannot add academic leave.");
        }

        var openLeave = await _leaveRepo.GetOpenByEnrollmentIdAsync(dto.EnrollmentId, ct);

        if (openLeave is not null)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} already has an open academic leave (leave #{openLeave.LeaveId}).");
        }

        var leave = dto.ToEntity();
        var created = await _leaveRepo.AddAsync(leave, ct);
        return created.ToDto();
    }
}

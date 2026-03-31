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
    private readonly IUnitOfWork _unitOfWork;

    public MovementService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var leaves = await _unitOfWork.AcademicLeaves.GetByStudentIdAsync(studentId, ct);
        var transfers = await _unitOfWork.ExternalTransfers.GetByStudentIdAsync(studentId, ct);

        return leaves.ToDto(transfers);
    }

    public async Task<ExternalTransferDto> CreateTransferAsync(int studentId, CreateTransferDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var institution = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, ct)
            ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

        var transferType = Enum.Parse<TransferType>(dto.TransferType, ignoreCase: true);
        var transfer = dto.ToEntity(studentId, transferType);
        var created = await _unitOfWork.ExternalTransfers.AddAsync(transfer, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return created.ToDto(institution.InstitutionName);
    }

    public async Task<AcademicLeaveDto> CreateLeaveAsync(int studentId, CreateLeaveDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(dto.EnrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), dto.EnrollmentId);

        if (enrollment.StudentId != studentId)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} does not belong to student {studentId}.");
        }

        if (enrollment.DateTo.HasValue)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} is already closed. Cannot add academic leave.");
        }

        var openLeave = await _unitOfWork.AcademicLeaves.GetOpenByEnrollmentIdAsync(dto.EnrollmentId, ct);

        if (openLeave is not null)
        {
            throw new DomainException($"Enrollment {dto.EnrollmentId} already has an open academic leave (leave #{openLeave.LeaveId}).");
        }

        var leave = dto.ToEntity();
        var created = await _unitOfWork.AcademicLeaves.AddAsync(leave, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return created.ToDto();
    }
}

using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IGroupRepository _groupRepo;
    private readonly IEnrollmentRepository _enrollmentRepo;

    public EnrollmentService(IStudentRepository studentRepo, IGroupRepository groupRepo,
        IEnrollmentRepository enrollmentRepo)
    {
        _studentRepo = studentRepo;
        _groupRepo = groupRepo;
        _enrollmentRepo = enrollmentRepo;
    }

    public async Task<int> EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(dto.StudentId, ct)
            ?? throw new NotFoundException(nameof(Student), dto.StudentId);
        _ = await _groupRepo.GetByIdAsync(dto.GroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.GroupId);

        var hasOverlap = await _enrollmentRepo.HasOverlapAsync(dto.StudentId, dto.DateFrom, null, null, ct);
        if (hasOverlap)
            throw new EnrollmentOverlapException(dto.StudentId, dto.DateFrom, null);

        var enrollment = new StudentGroupEnrollment
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            SubgroupId = dto.SubgroupId,
            DateFrom = dto.DateFrom,
            ReasonStart = dto.ReasonStart
        };

        var created = await _enrollmentRepo.AddAsync(enrollment, ct);
        return created.EnrollmentId;
    }

    public async Task CloseEnrollmentAsync(int enrollmentId, CloseEnrollmentDto dto, CancellationToken ct = default)
    {
        var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId, ct)
            ?? throw new NotFoundException(nameof(StudentGroupEnrollment), enrollmentId);

        if (enrollment.DateTo.HasValue)
            throw new DomainException($"Enrollment {enrollmentId} is already closed.");

        if (dto.DateTo < enrollment.DateFrom)
            throw new DomainException("DateTo cannot be before DateFrom.");

        enrollment.DateTo = dto.DateTo;
        enrollment.ReasonEnd = dto.ReasonEnd;
        await _enrollmentRepo.UpdateAsync(enrollment, ct);
    }
}

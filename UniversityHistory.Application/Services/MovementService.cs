using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetActiveAcademicDifference;
using UniversityHistory.Application.Queries.GetInternalTransferJournal;
using UniversityHistory.Application.Rules;
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
    private readonly IStudyProcessRule _studyProcessRule;

    public MovementService(
        IUnitOfWork unitOfWork,
        IGetActiveAcademicDifferenceQueryHandler activeAcademicDifferenceHandler,
        IGetInternalTransferJournalQueryHandler internalTransferJournalHandler,
        IStudyProcessRule studyProcessRule)
    {
        _unitOfWork = unitOfWork;
        _activeAcademicDifferenceHandler = activeAcademicDifferenceHandler;
        _internalTransferJournalHandler = internalTransferJournalHandler;
        _studyProcessRule = studyProcessRule;
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

        await _studyProcessRule.EnsureStudentModificationAllowedAsync(studentId, dto.TransferDate, ct);

        var institution = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, ct)
            ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

        var transferType = Enum.Parse<TransferType>(dto.TransferType, ignoreCase: true);
        var transfer = dto.ToEntity(studentId, transferType);
        var created = _unitOfWork.ExternalTransfers.Add(transfer);
        await _unitOfWork.SaveChangesAsync(ct);
        return created.ToDto(institution.InstitutionName);
    }

    public async Task<ExternalTransferDto> TransferOutAsync(Guid studentId, TransferStudentOutDto dto, CancellationToken ct = default)
    {
        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId, innerCt)
                ?? throw new NotFoundException(nameof(Student), studentId);

            _ = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, innerCt)
                ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

            var enrollment = await _unitOfWork.Enrollments.GetActiveByStudentIdAsync(studentId, innerCt)
                ?? throw new DomainException("The student has no active enrollment for external transfer.");

            await _studyProcessRule.EnsureEnrollmentModificationAllowedAsync(enrollment.EnrollmentId, dto.TransferDate, innerCt);

            if (dto.TransferDate < enrollment.DateFrom)
                throw new DomainException("The date of external transfer cannot be earlier than the start date of the current enrollment.");

            var existingCourses = (await _unitOfWork.StudyPlans
                .GetCourseEnrollmentsByEnrollmentIdAsync(enrollment.EnrollmentId, innerCt)).ToList();
            var plannedToRemove = existingCourses
                .Where(course => course.Status == CourseStatus.Planned && !course.GradeRecords.Any())
                .ToList();
            if (plannedToRemove.Count > 0)
            {
                _unitOfWork.StudyPlans.RemoveCourseEnrollments(plannedToRemove);
            }

            var openSubgroupEnrollment = await _unitOfWork.SubgroupEnrollments
                .GetOpenByEnrollmentIdAsync(enrollment.EnrollmentId, innerCt);
            if (openSubgroupEnrollment is not null)
            {
                if (dto.TransferDate < openSubgroupEnrollment.DateFrom)
                    throw new DomainException("The date of external transfer cannot be earlier than the start date of the current subgroup.");

                openSubgroupEnrollment.DateTo = dto.TransferDate;
                _unitOfWork.SubgroupEnrollments.Update(openSubgroupEnrollment);
            }

            enrollment.DateTo = dto.TransferDate;
            enrollment.ReasonEnd = dto.ReasonEnd;
            _unitOfWork.Enrollments.Update(enrollment);

            student.Status = StudentStatus.Expelled;
            _unitOfWork.Students.Update(student);

            var transfer = new ExternalTransfer
            {
                StudentId = studentId,
                InstitutionId = dto.InstitutionId,
                TransferType = TransferType.Out,
                TransferDate = dto.TransferDate,
                Notes = dto.Notes
            };

            _unitOfWork.ExternalTransfers.Add(transfer);
            await _unitOfWork.SaveChangesAsync(innerCt);

            var institution = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, innerCt)
                ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

            return transfer.ToDto(institution.InstitutionName);
        }, ct);
    }

    public async Task<StudentDto> ReturnFromExternalTransferAsync(
        Guid studentId,
        ReturnStudentFromExternalTransferDto dto,
        CancellationToken ct = default)
    {
        return await _unitOfWork.ExecuteInTransactionAsync(async innerCt =>
        {
            var student = await _unitOfWork.Students.GetByIdAsync(studentId, innerCt)
                ?? throw new NotFoundException(nameof(Student), studentId);

            _ = await _unitOfWork.ExternalTransfers.GetInstitutionByIdAsync(dto.InstitutionId, innerCt)
                ?? throw new NotFoundException(nameof(Institution), dto.InstitutionId);

            var activeEnrollment = await _unitOfWork.Enrollments.GetActiveByStudentIdAsync(studentId, innerCt);
            if (activeEnrollment is not null)
                throw new DomainException("Return from another university is not possible while the student has an active enrollment.");

            var transfers = await _unitOfWork.ExternalTransfers.GetByStudentIdAsync(studentId, innerCt);
            var hasTransferOut = transfers.Any(static transfer => transfer.TransferType == TransferType.Out);
            if (!hasTransferOut && student.Status != StudentStatus.Expelled)
            {
                throw new DomainException(
                    "Return from another university is only available for a student " +
                    "who was expelled or has a recorded external departure.");
            }

            student.Status = StudentStatus.Active;
            _unitOfWork.Students.Update(student);

            await EnrollStudentAsync(
                new EnrollStudentDto(
                    studentId,
                    dto.GroupId,
                    dto.SubgroupId,
                    dto.DateFrom,
                    dto.ReasonStart),
                innerCt);

            var transfer = new ExternalTransfer
            {
                StudentId = studentId,
                InstitutionId = dto.InstitutionId,
                TransferType = TransferType.In,
                TransferDate = dto.DateFrom,
                Notes = dto.Notes
            };

            _unitOfWork.ExternalTransfers.Add(transfer);
            await _unitOfWork.SaveChangesAsync(innerCt);

            return student.ToDto();
        }, ct);
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

        if (!dto.AllowRepeatedLeave && await _unitOfWork.AcademicLeaves.HasAnyByEnrollmentIdAsync(dto.EnrollmentId, ct))
        {
            throw new DomainException(
                "An academic leave has already been processed for this enrollment. " +
                "A repeated academic leave is only possible after a new enrollment.");
        }

        var openLeave = await _unitOfWork.AcademicLeaves.GetOpenByEnrollmentIdAsync(dto.EnrollmentId, ct);
        if (openLeave is not null)
            throw new DomainException($"Enrollment {dto.EnrollmentId} already has an open academic leave (leave #{openLeave.LeaveId}).");

        if (await _unitOfWork.AcademicLeaves.HasOverlapAsync(dto.EnrollmentId, dto.StartDate, dto.EndDate, ct: ct))
            throw new DomainException($"Academic leave for enrollment {dto.EnrollmentId} overlaps with an existing leave period.");

        var leave = dto.ToEntity();
        var created = _unitOfWork.AcademicLeaves.Add(leave);
        await _unitOfWork.SaveChangesAsync(ct);

        await RecalculateStudentLeaveStatusAsync(enrollment.StudentId, ct);
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
        await _unitOfWork.SaveChangesAsync(ct);

        await RecalculateStudentLeaveStatusAsync(leave.Enrollment.StudentId, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return leave.ToDto();
    }

    public async Task<AcademicLeaveDto> UpdateLeaveAsync(Guid leaveId, UpdateAcademicLeaveDto dto, CancellationToken ct = default)
    {
        var leave = await _unitOfWork.AcademicLeaves.GetByIdAsync(leaveId, ct)
            ?? throw new NotFoundException(nameof(AcademicLeave), leaveId);

        var enrollment = leave.Enrollment;

        if (dto.StartDate < enrollment.DateFrom)
            throw new DomainException("StartDate cannot be before the enrollment's DateFrom.");

        if (dto.EndDate.HasValue && dto.EndDate.Value < dto.StartDate)
            throw new DomainException("EndDate cannot be before StartDate.");

        if (enrollment.DateTo.HasValue)
        {
            if (dto.StartDate > enrollment.DateTo.Value)
                throw new DomainException("StartDate cannot be after the enrollment's DateTo.");

            if (dto.EndDate.HasValue && dto.EndDate.Value > enrollment.DateTo.Value)
                throw new DomainException("EndDate cannot be after the enrollment's DateTo.");
        }

        if (await _unitOfWork.AcademicLeaves.HasOverlapAsync(
                enrollment.EnrollmentId,
                dto.StartDate,
                dto.EndDate,
                leave.LeaveId,
                ct))
        {
            throw new DomainException($"Academic leave for enrollment {enrollment.EnrollmentId} overlaps with an existing leave period.");
        }

        leave.StartDate = dto.StartDate;
        leave.EndDate = dto.EndDate;
        leave.Reason = dto.Reason;
        leave.ReturnReason = dto.ReturnReason;
        _unitOfWork.AcademicLeaves.Update(leave);
        await _unitOfWork.SaveChangesAsync(ct);

        await RecalculateStudentLeaveStatusAsync(enrollment.StudentId, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return leave.ToDto();
    }

    private async Task RecalculateStudentLeaveStatusAsync(Guid studentId, CancellationToken ct)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var today = DateOnly.FromDateTime(DateTime.Today);
        var hasActiveLeaveToday = (await _unitOfWork.AcademicLeaves.GetByStudentIdAsync(studentId, ct))
            .Any(existing => existing.StartDate <= today && (!existing.EndDate.HasValue || existing.EndDate.Value >= today));

        if (hasActiveLeaveToday)
        {
            if (student.Status != StudentStatus.OnLeave)
            {
                student.Status = StudentStatus.OnLeave;
                _unitOfWork.Students.Update(student);
            }

            return;
        }

        if (student.Status == StudentStatus.OnLeave)
        {
            student.Status = StudentStatus.Active;
            _unitOfWork.Students.Update(student);
        }
    }

    private async Task EnrollStudentAsync(EnrollStudentDto dto, CancellationToken ct)
    {
        _ = await _unitOfWork.Students.GetByIdAsync(dto.StudentId, ct)
            ?? throw new NotFoundException(nameof(Student), dto.StudentId);
        var group = await _unitOfWork.Groups.GetByIdAsync(dto.GroupId, ct)
            ?? throw new NotFoundException(nameof(StudyGroup), dto.GroupId);

        if (dto.SubgroupId.HasValue && !group.Subgroups.Any(sg => sg.SubgroupId == dto.SubgroupId.Value))
            throw new DomainException($"Subgroup {dto.SubgroupId.Value} does not belong to Group {dto.GroupId}.");

        if (await _unitOfWork.Enrollments.HasOverlapAsync(dto.StudentId, dto.DateFrom, null, null, ct))
            throw new EnrollmentOverlapException(dto.StudentId, dto.DateFrom, null);

        var enrollment = new StudentGroupEnrollment
        {
            StudentId = dto.StudentId,
            GroupId = dto.GroupId,
            DateFrom = dto.DateFrom,
            ReasonStart = dto.ReasonStart
        };
        _unitOfWork.Enrollments.Add(enrollment);
        await _unitOfWork.SaveChangesAsync(ct);

        if (dto.SubgroupId.HasValue)
        {
            _unitOfWork.SubgroupEnrollments.Add(new StudentSubgroupEnrollment
            {
                EnrollmentId = enrollment.EnrollmentId,
                SubgroupId = dto.SubgroupId.Value,
                DateFrom = dto.DateFrom,
                DateTo = null,
                Reason = dto.ReasonStart
            });
        }

        var activePlan = await _unitOfWork.GroupPlanAssignments.GetActiveOnDateAsync(dto.GroupId, dto.DateFrom, ct);
        if (activePlan is not null)
        {
            var coveredDisciplineIds = await GetCoveredDisciplineIdsAsync(dto.StudentId, ct);
            var planDisciplinesDict = activePlan.Plan.PlanDisciplines.ToDictionary(pd => pd.PlanDisciplineId, pd => pd.DisciplineId);
            var courses = StudyPlanService.GenerateCourseEnrollments(
                    enrollment.EnrollmentId,
                    activePlan.GroupPlanAssignmentId,
                    dto.DateFrom,
                    activePlan.Plan)
                .Where(course => !coveredDisciplineIds.Contains(planDisciplinesDict[course.PlanDisciplineId]))
                .ToList();
            _unitOfWork.StudyPlans.AddCourseEnrollments(courses);
        }

        await _unitOfWork.SaveChangesAsync(ct);
    }

    private static HashSet<Guid> GetCoveredDisciplineIds(IEnumerable<StudentCourseEnrollment> courses)
    {
        return courses
            .Where(course => course.Status != CourseStatus.Planned || course.GradeRecords.Any())
            .Select(course => course.PlanDiscipline.DisciplineId)
            .ToHashSet();
    }

    private async Task<HashSet<Guid>> GetCoveredDisciplineIdsAsync(Guid studentId, CancellationToken ct)
    {
        var enrollments = await _unitOfWork.Enrollments.GetByStudentIdAsync(studentId, ct);
        var courses = new List<StudentCourseEnrollment>();

        foreach (var enrollment in enrollments)
        {
            var enrollmentCourses = await _unitOfWork.StudyPlans
                .GetCourseEnrollmentsByEnrollmentIdAsync(enrollment.EnrollmentId, ct);
            courses.AddRange(enrollmentCourses);
        }

        return GetCoveredDisciplineIds(courses);
    }
}

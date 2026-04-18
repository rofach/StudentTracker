using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class StudentMappingExtensions
{
    public static Student ToEntity(this StudentCreateDto dto)
    {
        return new Student
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            BirthDate = dto.BirthDate,
            Email = dto.Email,
            Phone = dto.Phone,
            Status = Domain.Enums.StudentStatus.Active
        };
    }

    public static StudentDto ToDto(this Student student)
    {
        return new StudentDto(
            student.StudentId,
            student.FirstName,
            student.LastName,
            student.Patronymic,
            student.BirthDate,
            student.Email,
            student.Phone,
            student.Status.ToString());
    }

    public static EnrollmentSummaryDto ToDto(this StudentGroupEnrollment enrollment)
    {
        var targetDate = enrollment.DateTo ?? DateOnly.FromDateTime(DateTime.Today);
        var currentSubgroup = enrollment.SubgroupEnrollments
            .Where(se => se.DateFrom <= targetDate && (!se.DateTo.HasValue || se.DateTo.Value >= targetDate))
            .OrderByDescending(se => se.DateFrom)
            .ThenByDescending(se => se.SubgroupEnrollmentId)
            .FirstOrDefault();

        return new EnrollmentSummaryDto(
            enrollment.EnrollmentId,
            enrollment.GroupId,
            enrollment.Group.GroupCode,
            enrollment.Group.Department.Name,
            enrollment.Group.Department.AcademicUnit.Name,
            enrollment.DateFrom,
            enrollment.DateTo,
            currentSubgroup?.SubgroupId,
            currentSubgroup?.Subgroup.SubgroupName);
    }

    public static StudentDetailDto ToDto(
        this Student student,
        IEnumerable<EnrollmentSummaryDto> enrollments,
        IEnumerable<GroupPlanAssignmentDto> plans,
        IEnumerable<AcademicLeaveDto> leaves,
        IEnumerable<ExternalTransferDto> transfers,
        bool isOnAcademicLeave,
        IEnumerable<StudentInternalTransferSummaryDto> internalTransfers)
    {
        return new StudentDetailDto(
            student.StudentId,
            student.FirstName,
            student.LastName,
            student.Patronymic,
            student.BirthDate,
            student.Email,
            student.Phone,
            student.Status.ToString(),
            isOnAcademicLeave,
            enrollments,
            plans,
            leaves,
            transfers,
            internalTransfers);
    }
}


using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class StudentCourseEnrollment
{
    public Guid CourseEnrollmentId { get; set; }
    public Guid EnrollmentId { get; set; }
    public Guid GroupPlanAssignmentId { get; set; }
    public Guid DisciplineId { get; set; }
    public int AcademicYearStart { get; set; }
    public CourseStatus Status { get; set; } = CourseStatus.Planned;

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
    public GroupPlanAssignment GroupPlanAssignment { get; set; } = null!;
    public Discipline Discipline { get; set; } = null!;
    public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
}


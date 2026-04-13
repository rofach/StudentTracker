using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class StudentCourseEnrollment
{
    public int CourseEnrollmentId { get; set; }
    public int EnrollmentId { get; set; }
    public int GroupPlanAssignmentId { get; set; }
    public int DisciplineId { get; set; }
    public int AcademicYearStart { get; set; }
    public CourseStatus Status { get; set; } = CourseStatus.Planned;

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
    public GroupPlanAssignment GroupPlanAssignment { get; set; } = null!;
    public Discipline Discipline { get; set; } = null!;
    public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
}

using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class StudentCourseEnrollment
{
    public int CourseEnrollmentId { get; set; }
    public int AssignmentId { get; set; }
    public int DisciplineId { get; set; }
    public int SemesterNo { get; set; }
    public CourseStatus Status { get; set; } = CourseStatus.Planned;

    public StudentPlanAssignment Assignment { get; set; } = null!;
    public Discipline Discipline { get; set; } = null!;
    public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
}

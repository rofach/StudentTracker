using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class PlanDiscipline
{
    public Guid PlanDisciplineId { get; set; }
    public Guid PlanId { get; set; }
    public Guid DisciplineId { get; set; }
    public int SemesterNo { get; set; }
    public ControlType ControlType { get; set; }
    public int Hours { get; set; }
    public decimal Credits { get; set; }

    public StudyPlan Plan { get; set; } = null!;
    public Discipline Discipline { get; set; } = null!;
    public ICollection<StudentCourseEnrollment> CourseEnrollments { get; set; } = new List<StudentCourseEnrollment>();
}


namespace UniversityHistory.Domain.Entities;

public class Discipline
{
    public Guid DisciplineId { get; set; }
    public string DisciplineName { get; set; } = string.Empty;

    public ICollection<PlanDiscipline> PlanDisciplines { get; set; } = new List<PlanDiscipline>();
    public ICollection<StudentCourseEnrollment> CourseEnrollments { get; set; } = new List<StudentCourseEnrollment>();
}


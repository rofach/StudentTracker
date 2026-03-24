namespace UniversityHistory.Domain.Entities;

public class StudentSubgroupAssignment
{
    public int EnrollmentId { get; set; }
    public int SubgroupId { get; set; }

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
    public Subgroup Subgroup { get; set; } = null!;
}

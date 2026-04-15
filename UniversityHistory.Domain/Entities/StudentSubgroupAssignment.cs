namespace UniversityHistory.Domain.Entities;

public class StudentSubgroupAssignment
{
    public Guid EnrollmentId { get; set; }
    public Guid SubgroupId { get; set; }

    public StudentGroupEnrollment Enrollment { get; set; } = null!;
    public Subgroup Subgroup { get; set; } = null!;
}


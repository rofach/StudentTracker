namespace UniversityHistory.Domain.Entities;

public class StudentGroupEnrollment
{
    public Guid EnrollmentId { get; set; }
    public Guid StudentId { get; set; }
    public Guid GroupId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public string ReasonStart { get; set; } = string.Empty;
    public string? ReasonEnd { get; set; }

    public Student Student { get; set; } = null!;
    public StudyGroup Group { get; set; } = null!;
    public StudentSubgroupAssignment? SubgroupAssignment { get; set; }
    public ICollection<AcademicLeave> AcademicLeaves { get; set; } = new List<AcademicLeave>();
    public ICollection<StudentCourseEnrollment> CourseEnrollments { get; set; } = new List<StudentCourseEnrollment>();
}


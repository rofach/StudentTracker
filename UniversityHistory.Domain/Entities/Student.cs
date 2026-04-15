using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Domain.Entities;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public StudentStatus Status { get; set; } = StudentStatus.Active;


    public ICollection<StudentGroupEnrollment> Enrollments { get; set; } = new List<StudentGroupEnrollment>();
    public ICollection<ExternalTransfer> ExternalTransfers { get; set; } = new List<ExternalTransfer>();
}

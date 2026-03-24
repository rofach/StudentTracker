using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Infrastructure.Data;

public class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudyGroup> StudyGroups => Set<StudyGroup>();
    public DbSet<Subgroup> Subgroups => Set<Subgroup>();
    public DbSet<Institution> Institutions => Set<Institution>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<StudyPlan> StudyPlans => Set<StudyPlan>();
    public DbSet<PlanDiscipline> PlanDisciplines => Set<PlanDiscipline>();
    public DbSet<StudentGroupEnrollment> StudentGroupEnrollments => Set<StudentGroupEnrollment>();
    public DbSet<AcademicLeave> AcademicLeaves => Set<AcademicLeave>();
    public DbSet<ExternalTransfer> ExternalTransfers => Set<ExternalTransfer>();
    public DbSet<StudentPlanAssignment> StudentPlanAssignments => Set<StudentPlanAssignment>();
    public DbSet<StudentCourseEnrollment> StudentCourseEnrollments => Set<StudentCourseEnrollment>();
    public DbSet<GradeRecord> GradeRecords => Set<GradeRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(e =>
        {
            e.ToTable("Student");
            e.HasKey(s => s.StudentId);
            e.Property(s => s.StudentId).HasColumnName("student_id").UseIdentityColumn();
            e.Property(s => s.FirstName).HasColumnName("first_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.LastName).HasColumnName("last_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.BirthDate).HasColumnName("birth_date");
            e.Property(s => s.Email).HasColumnName("email").HasMaxLength(100);
            e.Property(s => s.Phone).HasColumnName("phone").HasMaxLength(20);
            e.Property(s => s.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasConversion<string>()
                .HasDefaultValueSql("'Active'");
            e.ToTable(t => t.HasCheckConstraint(
                "chk_student_status",
                "status IN ('Active','OnLeave','Expelled','Graduated')"));
        });

        modelBuilder.Entity<StudyGroup>(e =>
        {
            e.ToTable("Study_Group");
            e.HasKey(g => g.GroupId);
            e.Property(g => g.GroupId).HasColumnName("group_id").UseIdentityColumn();
            e.Property(g => g.GroupCode).HasColumnName("group_code").HasMaxLength(20).IsRequired();
            e.HasIndex(g => g.GroupCode).IsUnique();
            e.Property(g => g.CreationYear).HasColumnName("creation_year").IsRequired();
            e.Property(g => g.Faculty).HasColumnName("faculty").HasMaxLength(100);
            e.Property(g => g.DateCreated).HasColumnName("date_created").IsRequired();
            e.Property(g => g.DateClosed).HasColumnName("date_closed");
        });

        modelBuilder.Entity<Subgroup>(e =>
        {
            e.ToTable("Subgroup");
            e.HasKey(s => s.SubgroupId);
            e.Property(s => s.SubgroupId).HasColumnName("subgroup_id").UseIdentityColumn();
            e.Property(s => s.SubgroupName).HasColumnName("subgroup_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.GroupId).HasColumnName("group_id");
            e.HasOne(s => s.Group)
                .WithMany(g => g.Subgroups)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Institution>(e =>
        {
            e.ToTable("Institution");
            e.HasKey(i => i.InstitutionId);
            e.Property(i => i.InstitutionId).HasColumnName("institution_id").UseIdentityColumn();
            e.Property(i => i.InstitutionName).HasColumnName("institution_name").HasMaxLength(200).IsRequired();
            e.Property(i => i.City).HasColumnName("city").HasMaxLength(100);
            e.Property(i => i.Country).HasColumnName("country").HasMaxLength(100);
        });

        modelBuilder.Entity<Discipline>(e =>
        {
            e.ToTable("Discipline");
            e.HasKey(d => d.DisciplineId);
            e.Property(d => d.DisciplineId).HasColumnName("discipline_id").UseIdentityColumn();
            e.Property(d => d.DisciplineName).HasColumnName("discipline_name").HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<StudyPlan>(e =>
        {
            e.ToTable("Study_Plan");
            e.HasKey(p => p.PlanId);
            e.Property(p => p.PlanId).HasColumnName("plan_id").UseIdentityColumn();
            e.Property(p => p.SpecialtyCode).HasColumnName("specialty_code").HasMaxLength(20).IsRequired();
            e.Property(p => p.PlanName).HasColumnName("plan_name").HasMaxLength(100);
            e.Property(p => p.TotalCredits).HasColumnName("total_credits").IsRequired();
            e.Property(p => p.ValidFrom).HasColumnName("valid_from").IsRequired();
        });

        modelBuilder.Entity<PlanDiscipline>(e =>
        {
            e.ToTable("Plan_Disciplines");
            e.HasKey(pd => new { pd.PlanId, pd.DisciplineId });
            e.Property(pd => pd.PlanId).HasColumnName("plan_id");
            e.Property(pd => pd.DisciplineId).HasColumnName("discipline_id");
            e.Property(pd => pd.SemesterNo).HasColumnName("semester_no").IsRequired();
            e.Property(pd => pd.ControlType)
                .HasColumnName("control_type")
                .HasMaxLength(20)
                .HasConversion<string>()
                .IsRequired();
            e.ToTable(t => t.HasCheckConstraint(
                "chk_control_type",
                "control_type IN ('Exam','Credit','Coursework')"));
            e.Property(pd => pd.Hours).HasColumnName("hours").IsRequired();
            e.Property(pd => pd.Credits).HasColumnName("credits").HasPrecision(4, 2).IsRequired();
            e.HasOne(pd => pd.Plan).WithMany(p => p.PlanDisciplines)
                .HasForeignKey(pd => pd.PlanId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(pd => pd.Discipline).WithMany(d => d.PlanDisciplines)
                .HasForeignKey(pd => pd.DisciplineId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudentGroupEnrollment>(e =>
        {
            e.ToTable("Student_Group_Enrollment");
            e.HasKey(en => en.EnrollmentId);
            e.Property(en => en.EnrollmentId).HasColumnName("enrollment_id").UseIdentityColumn();
            e.Property(en => en.StudentId).HasColumnName("student_id");
            e.Property(en => en.GroupId).HasColumnName("group_id");
            e.Property(en => en.SubgroupId).HasColumnName("subgroup_id");
            e.Property(en => en.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(en => en.DateTo).HasColumnName("date_to");
            e.Property(en => en.ReasonStart).HasColumnName("reason_start").HasMaxLength(50).IsRequired();
            e.Property(en => en.ReasonEnd).HasColumnName("reason_end").HasMaxLength(50);
            e.HasOne(en => en.Student).WithMany(s => s.Enrollments)
                .HasForeignKey(en => en.StudentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(en => en.Group).WithMany(g => g.Enrollments)
                .HasForeignKey(en => en.GroupId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(en => en.Subgroup).WithMany(sg => sg.Enrollments)
                .HasForeignKey(en => en.SubgroupId).OnDelete(DeleteBehavior.SetNull);
            e.HasIndex(en => new { en.GroupId, en.DateFrom })
                .HasDatabaseName("IX_Enrollment_GroupId_DateFrom");
        });

        modelBuilder.Entity<AcademicLeave>(e =>
        {
            e.ToTable("Academic_Leave");
            e.HasKey(l => l.LeaveId);
            e.Property(l => l.LeaveId).HasColumnName("leave_id").UseIdentityColumn();
            e.Property(l => l.StudentId).HasColumnName("student_id");
            e.Property(l => l.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(l => l.StartDate).HasColumnName("start_date").IsRequired();
            e.Property(l => l.EndDate).HasColumnName("end_date");
            e.Property(l => l.Reason).HasColumnName("reason").HasMaxLength(200);
            e.HasOne(l => l.Student).WithMany(s => s.AcademicLeaves)
                .HasForeignKey(l => l.StudentId).OnDelete(DeleteBehavior.NoAction);
            e.HasOne(l => l.Enrollment).WithMany(en => en.AcademicLeaves)
                .HasForeignKey(l => l.EnrollmentId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ExternalTransfer>(e =>
        {
            e.ToTable("External_Transfers");
            e.HasKey(t => t.TransferId);
            e.Property(t => t.TransferId).HasColumnName("transfer_id").UseIdentityColumn();
            e.Property(t => t.StudentId).HasColumnName("student_id");
            e.Property(t => t.InstitutionId).HasColumnName("institution_id");
            e.Property(t => t.TransferType)
                .HasColumnName("transfer_type")
                .HasMaxLength(3)
                .HasConversion<string>()
                .IsRequired();
            e.ToTable(t => t.HasCheckConstraint(
                "chk_transfer_type",
                "transfer_type IN ('In','Out')"));
            e.Property(t => t.TransferDate).HasColumnName("transfer_date").IsRequired();
            e.Property(t => t.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(t => t.Notes).HasColumnName("notes").HasMaxLength(200);
            e.HasOne(t => t.Student).WithMany(s => s.ExternalTransfers)
                .HasForeignKey(t => t.StudentId).OnDelete(DeleteBehavior.NoAction);
            e.HasOne(t => t.Institution).WithMany(i => i.Transfers)
                .HasForeignKey(t => t.InstitutionId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(t => t.Enrollment).WithMany(en => en.ExternalTransfers)
                .HasForeignKey(t => t.EnrollmentId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<StudentPlanAssignment>(e =>
        {
            e.ToTable("Student_Plan_Assignment");
            e.HasKey(a => a.AssignmentId);
            e.Property(a => a.AssignmentId).HasColumnName("assignment_id").UseIdentityColumn();
            e.Property(a => a.StudentId).HasColumnName("student_id");
            e.Property(a => a.PlanId).HasColumnName("plan_id");
            e.Property(a => a.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(a => a.DateTo).HasColumnName("date_to");
            e.HasOne(a => a.Student).WithMany(s => s.PlanAssignments)
                .HasForeignKey(a => a.StudentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(a => a.Plan).WithMany(p => p.PlanAssignments)
                .HasForeignKey(a => a.PlanId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudentCourseEnrollment>(e =>
        {
            e.ToTable("Student_Course_Enrollment");
            e.HasKey(ce => ce.CourseEnrollmentId);
            e.Property(ce => ce.CourseEnrollmentId).HasColumnName("course_enrollment_id").UseIdentityColumn();
            e.Property(ce => ce.AssignmentId).HasColumnName("assignment_id");
            e.Property(ce => ce.DisciplineId).HasColumnName("discipline_id");
            e.Property(ce => ce.SemesterNo).HasColumnName("semester_no").IsRequired();
            e.Property(ce => ce.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasConversion<string>()
                .HasDefaultValueSql("'Planned'");
            e.ToTable(t => t.HasCheckConstraint(
                "chk_course_status",
                "status IN ('Planned','InProgress','Completed','Retake')"));
            e.HasOne(ce => ce.Assignment).WithMany(a => a.CourseEnrollments)
                .HasForeignKey(ce => ce.AssignmentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ce => ce.Discipline).WithMany(d => d.CourseEnrollments)
                .HasForeignKey(ce => ce.DisciplineId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GradeRecord>(e =>
        {
            e.ToTable("Grade_Record");
            e.HasKey(gr => gr.GradeId);
            e.Property(gr => gr.GradeId).HasColumnName("grade_id").UseIdentityColumn();
            e.Property(gr => gr.CourseEnrollmentId).HasColumnName("course_enrollment_id");
            e.Property(gr => gr.GradeValue).HasColumnName("grade_value").HasMaxLength(20).IsRequired();
            e.Property(gr => gr.AssessmentDate).HasColumnName("assessment_date").IsRequired();
            e.HasIndex(gr => gr.CourseEnrollmentId).IsUnique();
            e.HasOne(gr => gr.CourseEnrollment).WithMany(ce => ce.GradeRecords)
                .HasForeignKey(gr => gr.CourseEnrollmentId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Seed();
    }
}

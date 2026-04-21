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
    public DbSet<GroupPlanAssignment> GroupPlanAssignments => Set<GroupPlanAssignment>();
    public DbSet<StudentCourseEnrollment> StudentCourseEnrollments => Set<StudentCourseEnrollment>();
    public DbSet<GradeRecord> GradeRecords => Set<GradeRecord>();
    public DbSet<StudentSubgroupEnrollment> StudentSubgroupEnrollments => Set<StudentSubgroupEnrollment>();
    public DbSet<AcademicUnit> AcademicUnits => Set<AcademicUnit>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<StudentGroupTransfer> StudentGroupTransfers => Set<StudentGroupTransfer>();
    public DbSet<AcademicDifferenceItem> AcademicDifferenceItems => Set<AcademicDifferenceItem>();
    public DbSet<StudentTimelineEventViewRow> StudentTimelineEvents => Set<StudentTimelineEventViewRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(e =>
        {
            e.ToTable("Student");
            e.HasKey(s => s.StudentId);
            e.Property(s => s.StudentId).HasColumnName("student_id");
            e.Property(s => s.FirstName).HasColumnName("first_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.LastName).HasColumnName("last_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.Patronymic).HasColumnName("patronymic").HasMaxLength(50);
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

        modelBuilder.Entity<AcademicUnit>(e =>
        {
            e.ToTable("Academic_Unit");
            e.HasKey(u => u.AcademicUnitId);
            e.Property(u => u.AcademicUnitId).HasColumnName("academic_unit_id");
            e.Property(u => u.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.HasIndex(u => u.Name).IsUnique();
            e.Property(u => u.Type)
                .HasColumnName("type")
                .HasMaxLength(20)
                .HasConversion<string>()
                .IsRequired();
            e.ToTable(t => t.HasCheckConstraint("chk_academic_unit_type", "type IN ('Faculty','Institute')"));
        });

        modelBuilder.Entity<Department>(e =>
        {
            e.ToTable("Department");
            e.HasKey(d => d.DepartmentId);
            e.Property(d => d.DepartmentId).HasColumnName("department_id");
            e.Property(d => d.AcademicUnitId).HasColumnName("academic_unit_id");
            e.Property(d => d.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.HasIndex(d => new { d.AcademicUnitId, d.Name }).IsUnique();
            e.HasOne(d => d.AcademicUnit).WithMany(u => u.Departments)
                .HasForeignKey(d => d.AcademicUnitId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudyGroup>(e =>
        {
            e.ToTable("Study_Group");
            e.HasKey(g => g.GroupId);
            e.Property(g => g.GroupId).HasColumnName("group_id");
            e.Property(g => g.GroupCode).HasColumnName("group_code").HasMaxLength(20).IsRequired();
            e.HasIndex(g => g.GroupCode).IsUnique();
            e.Property(g => g.DepartmentId).HasColumnName("department_id");
            e.Property(g => g.DateCreated).HasColumnName("date_created").IsRequired();
            e.Property(g => g.DateClosed).HasColumnName("date_closed");
            e.HasOne(g => g.Department).WithMany(d => d.Groups)
                .HasForeignKey(g => g.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Subgroup>(e =>
        {
            e.ToTable("Subgroup");
            e.HasKey(s => s.SubgroupId);
            e.Property(s => s.SubgroupId).HasColumnName("subgroup_id");
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
            e.Property(i => i.InstitutionId).HasColumnName("institution_id");
            e.Property(i => i.InstitutionName).HasColumnName("institution_name").HasMaxLength(200).IsRequired();
            e.Property(i => i.City).HasColumnName("city").HasMaxLength(100);
            e.Property(i => i.Country).HasColumnName("country").HasMaxLength(100);
        });

        modelBuilder.Entity<Discipline>(e =>
        {
            e.ToTable("Discipline");
            e.HasKey(d => d.DisciplineId);
            e.Property(d => d.DisciplineId).HasColumnName("discipline_id");
            e.Property(d => d.DisciplineName).HasColumnName("discipline_name").HasMaxLength(200).IsRequired();
            e.Property(d => d.Description).HasColumnName("description").HasMaxLength(1000);
        });

        modelBuilder.Entity<StudyPlan>(e =>
        {
            e.ToTable("Study_Plan");
            e.HasKey(p => p.PlanId);
            e.Property(p => p.PlanId).HasColumnName("plan_id");
            e.Property(p => p.SpecialtyCode).HasColumnName("specialty_code").HasMaxLength(20).IsRequired();
            e.Property(p => p.PlanName).HasColumnName("plan_name").HasMaxLength(100);
            e.Property(p => p.ValidFrom).HasColumnName("valid_from").IsRequired();
        });

        modelBuilder.Entity<PlanDiscipline>(e =>
        {
            e.ToTable("Plan_Disciplines");
            e.HasKey(pd => pd.PlanDisciplineId);
            e.Property(pd => pd.PlanDisciplineId).HasColumnName("plan_discipline_id");
            e.Property(pd => pd.PlanId).HasColumnName("plan_id");
            e.Property(pd => pd.DisciplineId).HasColumnName("discipline_id");
            e.HasIndex(pd => new { pd.PlanId, pd.DisciplineId }).IsUnique();
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
            e.Property(en => en.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(en => en.StudentId).HasColumnName("student_id");
            e.Property(en => en.GroupId).HasColumnName("group_id");
            e.Property(en => en.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(en => en.DateTo).HasColumnName("date_to");
            e.Property(en => en.ReasonStart).HasColumnName("reason_start").HasMaxLength(50).IsRequired();
            e.Property(en => en.ReasonEnd).HasColumnName("reason_end").HasMaxLength(50);
            e.HasOne(en => en.Student).WithMany(s => s.Enrollments)
                .HasForeignKey(en => en.StudentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(en => en.Group).WithMany(g => g.Enrollments)
                .HasForeignKey(en => en.GroupId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(en => new { en.GroupId, en.DateFrom })
                .HasDatabaseName("IX_Enrollment_GroupId_DateFrom");
        });

        modelBuilder.Entity<StudentSubgroupEnrollment>(e =>
        {
            e.ToTable("Student_Subgroup_Enrollment");
            e.HasKey(se => se.SubgroupEnrollmentId);
            e.Property(se => se.SubgroupEnrollmentId).HasColumnName("subgroup_enrollment_id");
            e.Property(se => se.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(se => se.SubgroupId).HasColumnName("subgroup_id");
            e.Property(se => se.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(se => se.DateTo).HasColumnName("date_to");
            e.Property(se => se.Reason).HasColumnName("reason").HasMaxLength(200).IsRequired();
            e.HasOne(se => se.Enrollment).WithMany(en => en.SubgroupEnrollments)
                .HasForeignKey(se => se.EnrollmentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(se => se.Subgroup).WithMany(s => s.SubgroupEnrollments)
                .HasForeignKey(se => se.SubgroupId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(se => new { se.EnrollmentId, se.DateFrom })
                .HasDatabaseName("IX_StudentSubgroupEnrollment_EnrollmentId_DateFrom");
            e.HasIndex(se => se.SubgroupId)
                .HasDatabaseName("IX_StudentSubgroupEnrollment_SubgroupId");
            e.HasIndex(se => se.EnrollmentId)
                .IsUnique()
                .HasFilter("[date_to] IS NULL")
                .HasDatabaseName("UX_StudentSubgroupEnrollment_Open");
        });

        modelBuilder.Entity<AcademicLeave>(e =>
        {
            e.ToTable("Academic_Leave");
            e.HasKey(l => l.LeaveId);
            e.Property(l => l.LeaveId).HasColumnName("leave_id");
            e.Property(l => l.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(l => l.StartDate).HasColumnName("start_date").IsRequired();
            e.Property(l => l.EndDate).HasColumnName("end_date");
            e.Property(l => l.Reason).HasColumnName("reason").HasMaxLength(200);
            e.Property(l => l.ReturnReason).HasColumnName("return_reason").HasMaxLength(200);
            e.HasOne(l => l.Enrollment).WithMany(en => en.AcademicLeaves)
                .HasForeignKey(l => l.EnrollmentId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ExternalTransfer>(e =>
        {
            e.ToTable("External_Transfers");
            e.HasKey(t => t.TransferId);
            e.Property(t => t.TransferId).HasColumnName("transfer_id");
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
            e.Property(t => t.Notes).HasColumnName("notes").HasMaxLength(200);
            e.HasOne(t => t.Student).WithMany(s => s.ExternalTransfers)
                .HasForeignKey(t => t.StudentId).OnDelete(DeleteBehavior.NoAction);
            e.HasOne(t => t.Institution).WithMany(i => i.Transfers)
                .HasForeignKey(t => t.InstitutionId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GroupPlanAssignment>(e =>
        {
            e.ToTable("Group_Plan_Assignment");
            e.HasKey(a => a.GroupPlanAssignmentId);
            e.Property(a => a.GroupPlanAssignmentId).HasColumnName("group_plan_assignment_id");
            e.Property(a => a.GroupId).HasColumnName("group_id");
            e.Property(a => a.PlanId).HasColumnName("plan_id");
            e.Property(a => a.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(a => a.DateTo).HasColumnName("date_to");
            e.HasOne(a => a.Group).WithMany(g => g.PlanAssignments)
                .HasForeignKey(a => a.GroupId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(a => a.Plan).WithMany(p => p.GroupPlanAssignments)
                .HasForeignKey(a => a.PlanId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(a => new { a.GroupId, a.DateFrom }).IsUnique()
                .HasDatabaseName("IX_GroupPlanAssignment_GroupId_DateFrom");
        });

        modelBuilder.Entity<StudentCourseEnrollment>(e =>
        {
            e.ToTable("Student_Course_Enrollment");
            e.HasKey(ce => ce.CourseEnrollmentId);
            e.Property(ce => ce.CourseEnrollmentId).HasColumnName("course_enrollment_id");
            e.Property(ce => ce.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(ce => ce.GroupPlanAssignmentId).HasColumnName("group_plan_assignment_id");
            e.Property(ce => ce.PlanDisciplineId).HasColumnName("plan_discipline_id");
            e.Property(ce => ce.AcademicYearStart).HasColumnName("academic_year_start").IsRequired();
            e.Property(ce => ce.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasConversion<string>()
                .HasDefaultValueSql("'Planned'");
            e.ToTable(t => t.HasCheckConstraint(
                "chk_course_status",
                "status IN ('Planned','InProgress','Completed','Retake')"));
            e.HasOne(ce => ce.Enrollment).WithMany(en => en.CourseEnrollments)
                .HasForeignKey(ce => ce.EnrollmentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ce => ce.GroupPlanAssignment).WithMany(a => a.StudentCourseEnrollments)
                .HasForeignKey(ce => ce.GroupPlanAssignmentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(ce => ce.PlanDiscipline).WithMany(pd => pd.CourseEnrollments)
                .HasForeignKey(ce => ce.PlanDisciplineId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GradeRecord>(e =>
        {
            e.ToTable("Grade_Record");
            e.HasKey(gr => gr.GradeId);
            e.Property(gr => gr.GradeId).HasColumnName("grade_id");
            e.Property(gr => gr.CourseEnrollmentId).HasColumnName("course_enrollment_id");
            e.Property(gr => gr.GradeValue).HasColumnName("grade_value").HasMaxLength(20).IsRequired();
            e.Property(gr => gr.AssessmentDate).HasColumnName("assessment_date").IsRequired();
            e.HasIndex(gr => gr.CourseEnrollmentId).IsUnique();
            e.HasOne(gr => gr.CourseEnrollment).WithMany(ce => ce.GradeRecords)
                .HasForeignKey(gr => gr.CourseEnrollmentId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentGroupTransfer>(e =>
        {
            e.ToTable("Student_Group_Transfer");
            e.HasKey(t => t.TransferId);
            e.Property(t => t.TransferId).HasColumnName("transfer_id");
            e.Property(t => t.OldEnrollmentId).HasColumnName("old_enrollment_id");
            e.Property(t => t.NewEnrollmentId).HasColumnName("new_enrollment_id");
            e.Property(t => t.TransferDate).HasColumnName("transfer_date").IsRequired();
            e.Property(t => t.Reason).HasColumnName("reason").HasMaxLength(200).IsRequired();
            e.HasOne(t => t.OldEnrollment).WithMany()
                .HasForeignKey(t => t.OldEnrollmentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(t => t.NewEnrollment).WithMany()
                .HasForeignKey(t => t.NewEnrollmentId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(t => t.OldEnrollmentId).HasDatabaseName("IX_StudentGroupTransfer_OldEnrollmentId");
            e.HasIndex(t => t.NewEnrollmentId).HasDatabaseName("IX_StudentGroupTransfer_NewEnrollmentId");
        });

        modelBuilder.Entity<AcademicDifferenceItem>(e =>
        {
            e.ToTable("Academic_Difference_Item");
            e.HasKey(d => d.DifferenceItemId);
            e.Property(d => d.DifferenceItemId).HasColumnName("difference_item_id");
            e.Property(d => d.TransferId).HasColumnName("transfer_id");
            e.Property(d => d.PlanDisciplineId).HasColumnName("plan_discipline_id");
            e.Property(d => d.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasConversion<string>()
                .HasDefaultValueSql("'Pending'");
            e.ToTable(t => t.HasCheckConstraint(
                "chk_diff_item_status",
                "status IN ('Pending','Completed','Waived')"));
            e.Property(d => d.Notes).HasColumnName("notes").HasMaxLength(500);
            e.HasOne(d => d.Transfer).WithMany(t => t.DifferenceItems)
                .HasForeignKey(d => d.TransferId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(d => d.PlanDiscipline).WithMany()
                .HasForeignKey(d => d.PlanDisciplineId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(d => new { d.TransferId, d.PlanDisciplineId }).IsUnique()
                .HasDatabaseName("UX_AcademicDifferenceItem_TransferId_PlanDisciplineId");
        });

        modelBuilder.Entity<StudentTimelineEventViewRow>(e =>
        {
            e.HasNoKey();
            e.ToView("vw_StudentTimeline");
            e.Property(x => x.StudentId).HasColumnName("StudentId");
            e.Property(x => x.EventType).HasColumnName("EventType");
            e.Property(x => x.Description).HasColumnName("Description");
            e.Property(x => x.DateFrom).HasColumnName("DateFrom");
            e.Property(x => x.DateTo).HasColumnName("DateTo");
            e.Property(x => x.GroupCode).HasColumnName("GroupCode");
            e.Property(x => x.DepartmentName).HasColumnName("DepartmentName");
            e.Property(x => x.AcademicUnitName).HasColumnName("AcademicUnitName");
            e.Property(x => x.AcademicUnitType).HasColumnName("AcademicUnitType");
            e.Property(x => x.SortPriority).HasColumnName("SortPriority");
            e.Property(x => x.EventKey).HasColumnName("EventKey");
        });

        modelBuilder.Seed();
    }
}



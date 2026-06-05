using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Infrastructure.Identity;

namespace UniversityHistory.Infrastructure.Data;

public class UniversityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("asp_net_roles");
            entity.HasKey(role => role.Id).HasName("pk_asp_net_roles");
            entity.Property(role => role.Id).HasColumnName("id");
            entity.Property(role => role.Name).HasColumnName("name").HasMaxLength(256);
            entity.Property(role => role.NormalizedName).HasColumnName("normalized_name").HasMaxLength(256);
            entity.Property(role => role.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            entity.HasIndex(role => role.NormalizedName)
                .IsUnique()
                .HasDatabaseName("ix_asp_net_roles_normalized_name")
                .HasFilter("[normalized_name] IS NOT NULL");
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("asp_net_user_roles");
            entity.HasKey(userRole => new { userRole.UserId, userRole.RoleId }).HasName("pk_asp_net_user_roles");
            entity.Property(userRole => userRole.UserId).HasColumnName("user_id");
            entity.Property(userRole => userRole.RoleId).HasColumnName("role_id");
        });

        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("asp_net_user_claims");
            entity.HasKey(claim => claim.Id).HasName("pk_asp_net_user_claims");
            entity.Property(claim => claim.Id).HasColumnName("id");
            entity.Property(claim => claim.UserId).HasColumnName("user_id");
            entity.Property(claim => claim.ClaimType).HasColumnName("claim_type");
            entity.Property(claim => claim.ClaimValue).HasColumnName("claim_value");
        });

        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("asp_net_user_logins");
            entity.HasKey(login => new { login.LoginProvider, login.ProviderKey }).HasName("pk_asp_net_user_logins");
            entity.Property(login => login.LoginProvider).HasColumnName("login_provider");
            entity.Property(login => login.ProviderKey).HasColumnName("provider_key");
            entity.Property(login => login.ProviderDisplayName).HasColumnName("provider_display_name");
            entity.Property(login => login.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("asp_net_role_claims");
            entity.HasKey(claim => claim.Id).HasName("pk_asp_net_role_claims");
            entity.Property(claim => claim.Id).HasColumnName("id");
            entity.Property(claim => claim.RoleId).HasColumnName("role_id");
            entity.Property(claim => claim.ClaimType).HasColumnName("claim_type");
            entity.Property(claim => claim.ClaimValue).HasColumnName("claim_value");
        });

        modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("asp_net_user_tokens");
            entity.HasKey(token => new { token.UserId, token.LoginProvider, token.Name }).HasName("pk_asp_net_user_tokens");
            entity.Property(token => token.UserId).HasColumnName("user_id");
            entity.Property(token => token.LoginProvider).HasColumnName("login_provider");
            entity.Property(token => token.Name).HasColumnName("name");
            entity.Property(token => token.Value).HasColumnName("value");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("asp_net_users");
            entity.HasKey(user => user.Id).HasName("pk_asp_net_users");
            entity.Property(user => user.Id).HasColumnName("id");
            entity.Property(user => user.UserName).HasColumnName("user_name").HasMaxLength(256);
            entity.Property(user => user.NormalizedUserName).HasColumnName("normalized_user_name").HasMaxLength(256);
            entity.Property(user => user.Email).HasColumnName("email").HasMaxLength(256);
            entity.Property(user => user.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(256);
            entity.Property(user => user.EmailConfirmed).HasColumnName("email_confirmed");
            entity.Property(user => user.PasswordHash).HasColumnName("password_hash");
            entity.Property(user => user.SecurityStamp).HasColumnName("security_stamp");
            entity.Property(user => user.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            entity.Property(user => user.PhoneNumber).HasColumnName("phone_number");
            entity.Property(user => user.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
            entity.Property(user => user.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            entity.Property(user => user.LockoutEnd).HasColumnName("lockout_end");
            entity.Property(user => user.LockoutEnabled).HasColumnName("lockout_enabled");
            entity.Property(user => user.AccessFailedCount).HasColumnName("access_failed_count");
            entity.Property(user => user.StudentId)
                .HasColumnName("student_id");

            entity.HasIndex(user => user.NormalizedEmail)
                .HasDatabaseName("ix_asp_net_users_normalized_email");

            entity.HasIndex(user => user.NormalizedUserName)
                .IsUnique()
                .HasDatabaseName("ix_asp_net_users_normalized_user_name")
                .HasFilter("[normalized_user_name] IS NOT NULL");

            entity.HasIndex(user => user.StudentId)
                .IsUnique()
                .HasFilter("[student_id] IS NOT NULL")
                .HasDatabaseName("ix_asp_net_users_student_id");

            entity.HasOne<Student>()
                .WithOne()
                .HasForeignKey<ApplicationUser>(user => user.StudentId)
                .HasConstraintName("fk_asp_net_users_student_student_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(e =>
        {
            e.ToTable("student");
            e.HasKey(s => s.StudentId).HasName("pk_student");
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
            e.ToTable("academic_unit");
            e.HasKey(u => u.AcademicUnitId).HasName("pk_academic_unit");
            e.Property(u => u.AcademicUnitId).HasColumnName("academic_unit_id");
            e.Property(u => u.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.HasIndex(u => u.Name).IsUnique().HasDatabaseName("ix_academic_unit_name");
            e.Property(u => u.Type)
                .HasColumnName("type")
                .HasMaxLength(20)
                .HasConversion<string>()
                .IsRequired();
            e.ToTable(t => t.HasCheckConstraint("chk_academic_unit_type", "type IN ('Faculty','Institute')"));
        });

        modelBuilder.Entity<Department>(e =>
        {
            e.ToTable("department");
            e.HasKey(d => d.DepartmentId).HasName("pk_department");
            e.Property(d => d.DepartmentId).HasColumnName("department_id");
            e.Property(d => d.AcademicUnitId).HasColumnName("academic_unit_id");
            e.Property(d => d.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            e.HasIndex(d => new { d.AcademicUnitId, d.Name }).IsUnique().HasDatabaseName("ix_department_academic_unit_id_name");
            e.HasOne(d => d.AcademicUnit).WithMany(u => u.Departments)
                .HasForeignKey(d => d.AcademicUnitId)
                .HasConstraintName("fk_department_academic_unit_academic_unit_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudyGroup>(e =>
        {
            e.ToTable("study_group");
            e.HasKey(g => g.GroupId).HasName("pk_study_group");
            e.Property(g => g.GroupId).HasColumnName("group_id");
            e.Property(g => g.GroupCode).HasColumnName("group_code").HasMaxLength(20).IsRequired();
            e.HasIndex(g => g.GroupCode).IsUnique().HasDatabaseName("ix_study_group_group_code");
            e.Property(g => g.DepartmentId).HasColumnName("department_id");
            e.Property(g => g.DateCreated).HasColumnName("date_created").IsRequired();
            e.Property(g => g.DateClosed).HasColumnName("date_closed");
            e.HasOne(g => g.Department).WithMany(d => d.Groups)
                .HasForeignKey(g => g.DepartmentId)
                .HasConstraintName("fk_study_group_department_department_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Subgroup>(e =>
        {
            e.ToTable("subgroup");
            e.HasKey(s => s.SubgroupId).HasName("pk_subgroup");
            e.Property(s => s.SubgroupId).HasColumnName("subgroup_id");
            e.Property(s => s.SubgroupName).HasColumnName("subgroup_name").HasMaxLength(50).IsRequired();
            e.Property(s => s.GroupId).HasColumnName("group_id");
            e.HasOne(s => s.Group)
                .WithMany(g => g.Subgroups)
                .HasForeignKey(s => s.GroupId)
                .HasConstraintName("fk_subgroup_study_group_group_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Institution>(e =>
        {
            e.ToTable("institution");
            e.HasKey(i => i.InstitutionId).HasName("pk_institution");
            e.Property(i => i.InstitutionId).HasColumnName("institution_id");
            e.Property(i => i.InstitutionName).HasColumnName("institution_name").HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Discipline>(e =>
        {
            e.ToTable("discipline");
            e.HasKey(d => d.DisciplineId).HasName("pk_discipline");
            e.Property(d => d.DisciplineId).HasColumnName("discipline_id");
            e.Property(d => d.DisciplineName).HasColumnName("discipline_name").HasMaxLength(200).IsRequired();
            e.Property(d => d.Description).HasColumnName("description").HasMaxLength(1000);
        });

        modelBuilder.Entity<StudyPlan>(e =>
        {
            e.ToTable("study_plan");
            e.HasKey(p => p.PlanId).HasName("pk_study_plan");
            e.Property(p => p.PlanId).HasColumnName("plan_id");
            e.Property(p => p.SpecialtyCode).HasColumnName("specialty_code").HasMaxLength(20).IsRequired();
            e.Property(p => p.PlanName).HasColumnName("plan_name").HasMaxLength(100);
            e.Property(p => p.ValidFrom).HasColumnName("valid_from").IsRequired();
        });

        modelBuilder.Entity<PlanDiscipline>(e =>
        {
            e.ToTable("plan_disciplines");
            e.HasKey(pd => pd.PlanDisciplineId).HasName("pk_plan_disciplines");
            e.Property(pd => pd.PlanDisciplineId).HasColumnName("plan_discipline_id");
            e.Property(pd => pd.PlanId).HasColumnName("plan_id");
            e.Property(pd => pd.DisciplineId).HasColumnName("discipline_id");
            e.HasIndex(pd => new { pd.PlanId, pd.DisciplineId }).IsUnique().HasDatabaseName("ix_plan_disciplines_plan_id_discipline_id");
            e.Property(pd => pd.SemesterNo).HasColumnName("semester_no").IsRequired();
            e.Property(pd => pd.ControlType)
                .HasColumnName("control_type")
                .HasMaxLength(20)
                .HasConversion<string>()
                .IsRequired();
            e.ToTable(t => t.HasCheckConstraint(
                "chk_control_type",
                "control_type IN ('Exam','Credit','Coursework')"));
            e.Property(pd => pd.Credits).HasColumnName("credits").HasPrecision(4, 2).IsRequired();
            e.HasOne(pd => pd.Plan).WithMany(p => p.PlanDisciplines)
                .HasForeignKey(pd => pd.PlanId)
                .HasConstraintName("fk_plan_disciplines_study_plan_plan_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(pd => pd.Discipline).WithMany(d => d.PlanDisciplines)
                .HasForeignKey(pd => pd.DisciplineId)
                .HasConstraintName("fk_plan_disciplines_discipline_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<StudentGroupEnrollment>(e =>
        {
            e.ToTable("student_group_enrollment");
            e.HasKey(en => en.EnrollmentId).HasName("pk_student_group_enrollment");
            e.Property(en => en.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(en => en.StudentId).HasColumnName("student_id");
            e.Property(en => en.GroupId).HasColumnName("group_id");
            e.Property(en => en.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(en => en.DateTo).HasColumnName("date_to");
            e.Property(en => en.ReasonStart).HasColumnName("reason_start").HasMaxLength(50).IsRequired();
            e.Property(en => en.ReasonEnd).HasColumnName("reason_end").HasMaxLength(50);
            e.HasOne(en => en.Student).WithMany(s => s.Enrollments)
                .HasForeignKey(en => en.StudentId)
                .HasConstraintName("fk_student_group_enrollment_student_student_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(en => en.Group).WithMany(g => g.Enrollments)
                .HasForeignKey(en => en.GroupId)
                .HasConstraintName("fk_student_group_enrollment_study_group_group_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(en => new { en.GroupId, en.DateFrom })
                .HasDatabaseName("ix_student_group_enrollment_group_id_date_from");
        });

        modelBuilder.Entity<StudentSubgroupEnrollment>(e =>
        {
            e.ToTable("student_subgroup_enrollment");
            e.HasKey(se => se.SubgroupEnrollmentId).HasName("pk_student_subgroup_enrollment");
            e.Property(se => se.SubgroupEnrollmentId).HasColumnName("subgroup_enrollment_id");
            e.Property(se => se.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(se => se.SubgroupId).HasColumnName("subgroup_id");
            e.Property(se => se.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(se => se.DateTo).HasColumnName("date_to");
            e.Property(se => se.Reason).HasColumnName("reason").HasMaxLength(200).IsRequired();
            e.HasOne(se => se.Enrollment).WithMany(en => en.SubgroupEnrollments)
                .HasForeignKey(se => se.EnrollmentId)
                .HasConstraintName("fk_student_subgroup_enrollment_student_group_enrollment_enrollment_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(se => se.Subgroup).WithMany(s => s.SubgroupEnrollments)
                .HasForeignKey(se => se.SubgroupId)
                .HasConstraintName("fk_student_subgroup_enrollment_subgroup_subgroup_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(se => new { se.EnrollmentId, se.DateFrom })
                .HasDatabaseName("ix_student_subgroup_enrollment_enrollment_id_date_from");
            e.HasIndex(se => se.SubgroupId)
                .HasDatabaseName("ix_student_subgroup_enrollment_subgroup_id");
            e.HasIndex(se => se.EnrollmentId)
                .IsUnique()
                .HasFilter("[date_to] IS NULL")
                .HasDatabaseName("ix_student_subgroup_enrollment_open");
        });

        modelBuilder.Entity<AcademicLeave>(e =>
        {
            e.ToTable("academic_leave");
            e.HasKey(l => l.LeaveId).HasName("pk_academic_leave");
            e.Property(l => l.LeaveId).HasColumnName("leave_id");
            e.Property(l => l.EnrollmentId).HasColumnName("enrollment_id");
            e.Property(l => l.StartDate).HasColumnName("start_date").IsRequired();
            e.Property(l => l.EndDate).HasColumnName("end_date");
            e.Property(l => l.Reason).HasColumnName("reason").HasMaxLength(200);
            e.Property(l => l.ReturnReason).HasColumnName("return_reason").HasMaxLength(200);
            e.HasOne(l => l.Enrollment).WithMany(en => en.AcademicLeaves)
                .HasForeignKey(l => l.EnrollmentId)
                .HasConstraintName("fk_academic_leave_student_group_enrollment_enrollment_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExternalTransfer>(e =>
        {
            e.ToTable("external_transfers");
            e.HasKey(t => t.TransferId).HasName("pk_external_transfers");
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
                .HasForeignKey(t => t.StudentId)
                .HasConstraintName("fk_external_transfers_student_student_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(t => t.Institution).WithMany(i => i.Transfers)
                .HasForeignKey(t => t.InstitutionId)
                .HasConstraintName("fk_external_transfers_institution_institution_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GroupPlanAssignment>(e =>
        {
            e.ToTable("group_plan_assignment");
            e.HasKey(a => a.GroupPlanAssignmentId).HasName("pk_group_plan_assignment");
            e.Property(a => a.GroupPlanAssignmentId).HasColumnName("group_plan_assignment_id");
            e.Property(a => a.GroupId).HasColumnName("group_id");
            e.Property(a => a.PlanId).HasColumnName("plan_id");
            e.Property(a => a.DateFrom).HasColumnName("date_from").IsRequired();
            e.Property(a => a.DateTo).HasColumnName("date_to");
            e.HasOne(a => a.Group).WithMany(g => g.PlanAssignments)
                .HasForeignKey(a => a.GroupId)
                .HasConstraintName("fk_group_plan_assignment_study_group_group_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(a => a.Plan).WithMany(p => p.GroupPlanAssignments)
                .HasForeignKey(a => a.PlanId)
                .HasConstraintName("fk_group_plan_assignment_study_plan_plan_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(a => new { a.GroupId, a.DateFrom }).IsUnique()
                .HasDatabaseName("ix_group_plan_assignment_group_id_date_from");
        });

        modelBuilder.Entity<StudentCourseEnrollment>(e =>
        {
            e.ToTable("student_course_enrollment");
            e.HasKey(ce => ce.CourseEnrollmentId).HasName("pk_student_course_enrollment");
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
                .HasForeignKey(ce => ce.EnrollmentId)
                .HasConstraintName("fk_student_course_enrollment_student_group_enrollment_enrollment_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ce => ce.GroupPlanAssignment).WithMany(a => a.StudentCourseEnrollments)
                .HasForeignKey(ce => ce.GroupPlanAssignmentId)
                .HasConstraintName("fk_student_course_enrollment_group_plan_assignment_group_plan_assignment_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(ce => ce.PlanDiscipline).WithMany(pd => pd.CourseEnrollments)
                .HasForeignKey(ce => ce.PlanDisciplineId)
                .HasConstraintName("fk_student_course_enrollment_plan_disciplines_plan_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<GradeRecord>(e =>
        {
            e.ToTable("grade_record");
            e.HasKey(gr => gr.GradeId).HasName("pk_grade_record");
            e.Property(gr => gr.GradeId).HasColumnName("grade_id");
            e.Property(gr => gr.CourseEnrollmentId).HasColumnName("course_enrollment_id");
            e.Property(gr => gr.GradeValue).HasColumnName("grade_value").IsRequired();
            e.Property(gr => gr.AssessmentDate).HasColumnName("assessment_date").IsRequired();
            e.HasIndex(gr => gr.CourseEnrollmentId).IsUnique().HasDatabaseName("ix_grade_record_course_enrollment_id");
            e.HasOne(gr => gr.CourseEnrollment).WithMany(ce => ce.GradeRecords)
                .HasForeignKey(gr => gr.CourseEnrollmentId)
                .HasConstraintName("fk_grade_record_student_course_enrollment_course_enrollment_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentGroupTransfer>(e =>
        {
            e.ToTable("student_group_transfer");
            e.HasKey(t => t.TransferId).HasName("pk_student_group_transfer");
            e.Property(t => t.TransferId).HasColumnName("transfer_id");
            e.Property(t => t.OldEnrollmentId).HasColumnName("old_enrollment_id");
            e.Property(t => t.NewEnrollmentId).HasColumnName("new_enrollment_id");
            e.Property(t => t.TransferDate).HasColumnName("transfer_date").IsRequired();
            e.Property(t => t.Reason).HasColumnName("reason").HasMaxLength(200).IsRequired();
            e.HasOne(t => t.OldEnrollment).WithMany()
                .HasForeignKey(t => t.OldEnrollmentId)
                .HasConstraintName("fk_student_group_transfer_student_group_enrollment_old_enrollment_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(t => t.NewEnrollment).WithMany()
                .HasForeignKey(t => t.NewEnrollmentId)
                .HasConstraintName("fk_student_group_transfer_student_group_enrollment_new_enrollment_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(t => t.OldEnrollmentId).HasDatabaseName("ix_student_group_transfer_old_enrollment_id");
            e.HasIndex(t => t.NewEnrollmentId).HasDatabaseName("ix_student_group_transfer_new_enrollment_id");
        });

        modelBuilder.Entity<AcademicDifferenceItem>(e =>
        {
            e.ToTable("academic_difference_item");
            e.HasKey(d => d.DifferenceItemId).HasName("pk_academic_difference_item");
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
                .HasForeignKey(d => d.TransferId)
                .HasConstraintName("fk_academic_difference_item_student_group_transfer_transfer_id")
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(d => d.PlanDiscipline).WithMany()
                .HasForeignKey(d => d.PlanDisciplineId)
                .HasConstraintName("fk_academic_difference_item_plan_disciplines_plan_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(d => new { d.TransferId, d.PlanDisciplineId }).IsUnique()
                .HasDatabaseName("ix_academic_difference_item_transfer_id_plan_discipline_id");
        });

        modelBuilder.Entity<StudentTimelineEventViewRow>(e =>
        {
            e.HasNoKey();
            e.ToView("vw_student_timeline");
            e.Property(x => x.StudentId).HasColumnName("student_id");
            e.Property(x => x.EventType).HasColumnName("event_type");
            e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.DateFrom).HasColumnName("date_from");
            e.Property(x => x.DateTo).HasColumnName("date_to");
            e.Property(x => x.GroupCode).HasColumnName("group_code");
            e.Property(x => x.DepartmentName).HasColumnName("department_name");
            e.Property(x => x.AcademicUnitName).HasColumnName("academic_unit_name");
            e.Property(x => x.AcademicUnitType).HasColumnName("academic_unit_type");
            e.Property(x => x.EventOrder).HasColumnName("event_order");
            e.Property(x => x.SortPriority).HasColumnName("sort_priority");
            e.Property(x => x.EventKey).HasColumnName("event_key");
        });

        modelBuilder.Seed();
    }
}



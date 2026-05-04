using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniversityHistory.Infrastructure.Identity;

namespace UniversityHistory.Infrastructure.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(user => user.StudentId)
                .HasColumnName("student_id");

            entity.HasIndex(user => user.StudentId)
                .IsUnique()
                .HasFilter("[student_id] IS NOT NULL");
        });
    }
}

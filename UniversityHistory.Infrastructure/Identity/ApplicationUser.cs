using Microsoft.AspNetCore.Identity;

namespace UniversityHistory.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid? StudentId { get; set; }
}

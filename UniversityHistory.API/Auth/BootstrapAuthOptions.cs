namespace UniversityHistory.API.Auth;

public class BootstrapAuthOptions
{
    public const string SectionName = "BootstrapAuth";

    public List<BootstrapUserOptions> Users { get; set; } = [];
}

public class BootstrapUserOptions
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = AuthRoles.Student;
    public Guid? StudentId { get; set; }
}

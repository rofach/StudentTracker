using UniversityHistory.API.Services;

namespace UniversityHistory.API.Extensions;

public static class IdentitySeederExtensions
{
    public static async Task SeedIdentityAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var identitySeeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();
        await identitySeeder.SeedAsync();
    }
}

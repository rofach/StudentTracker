using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniversityHistory.Infrastructure.Data;

public class UniversityDbContextFactory : IDesignTimeDbContextFactory<UniversityDbContext>
{
    public UniversityDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<UniversityDbContext>()
            .UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=UniversityHistoryDb;Trusted_Connection=True;",
                sql => sql.MigrationsAssembly("UniversityHistory.Infrastructure"))
            .Options;

        return new UniversityDbContext(options);
    }
}


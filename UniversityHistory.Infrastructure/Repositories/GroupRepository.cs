using Microsoft.EntityFrameworkCore;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Interfaces.Repositories;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly UniversityDbContext _db;
    public GroupRepository(UniversityDbContext db)
    {
        _db = db;
    }

    public async Task<StudyGroup?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.StudyGroups
            .Include(g => g.Subgroups)
            .Include(g => g.Department)
                .ThenInclude(d => d.AcademicUnit)
            .FirstOrDefaultAsync(g => g.GroupId == id, ct);
    }

    public async Task<IEnumerable<StudyGroup>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.StudyGroups.AsNoTracking().ToListAsync(ct);
    }

    public StudyGroup Add(StudyGroup group)
    {
        _db.StudyGroups.Add(group);
        return group;
    }

    public async Task<bool> GroupCodeExistsAsync(string groupCode, Guid? excludeId, CancellationToken ct = default)
    {
        return await _db.StudyGroups
            .AnyAsync(g => g.GroupCode == groupCode && g.GroupId != excludeId, ct);
    }

    public void Update(StudyGroup group)
    {
        _db.StudyGroups.Update(group);
    }
}


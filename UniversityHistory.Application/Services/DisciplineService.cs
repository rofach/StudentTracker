using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class DisciplineService : IDisciplineService
{
    private readonly IDisciplineRepository _repo;

    public DisciplineService(IDisciplineRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<DisciplineDto>> GetAllAsync(CancellationToken ct = default)
    {
        var all = await _repo.GetAllAsync(ct);
        return all.Select(static discipline => discipline.ToDto());
    }

    public async Task<DisciplineDto?> GetByIdAsync(int disciplineId, CancellationToken ct = default)
    {
        var discipline = await _repo.GetByIdAsync(disciplineId, ct);
        return discipline is null ? null : discipline.ToDto();
    }

    public async Task<DisciplineDto> CreateAsync(CreateDisciplineDto dto, CancellationToken ct = default)
    {
        if (await _repo.ExistsWithNameAsync(dto.DisciplineName, ct: ct))
        {
            throw new DomainException($"A discipline named '{dto.DisciplineName}' already exists.");
        }

        var discipline = dto.ToEntity();
        return (await _repo.AddAsync(discipline, ct)).ToDto();
    }

    public async Task<DisciplineDto> UpdateAsync(int disciplineId, UpdateDisciplineDto dto, CancellationToken ct = default)
    {
        var discipline = await _repo.GetByIdAsync(disciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), disciplineId);

        if (await _repo.ExistsWithNameAsync(dto.DisciplineName, excludeId: disciplineId, ct: ct))
        {
            throw new DomainException($"A discipline named '{dto.DisciplineName}' already exists.");
        }

        discipline.DisciplineName = dto.DisciplineName;
        await _repo.UpdateAsync(discipline, ct);
        return discipline.ToDto();
    }

    public async Task DeleteAsync(int disciplineId, CancellationToken ct = default)
    {
        var discipline = await _repo.GetByIdAsync(disciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), disciplineId);

        if (await _repo.IsUsedInPlanAsync(disciplineId, ct))
        {
            throw new DomainException($"Cannot delete discipline {disciplineId}: it is referenced by one or more study plans.");
        }

        await _repo.DeleteAsync(discipline, ct);
    }
}

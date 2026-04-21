using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Application.Mappings;
using UniversityHistory.Application.Queries.GetDisciplineSearch;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class DisciplineService : IDisciplineService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGetDisciplineSearchQueryHandler _disciplineSearchHandler;

    public DisciplineService(
        IUnitOfWork unitOfWork,
        IGetDisciplineSearchQueryHandler disciplineSearchHandler)
    {
        _unitOfWork = unitOfWork;
        _disciplineSearchHandler = disciplineSearchHandler;
    }

    public async Task<IEnumerable<DisciplineDto>> GetAllAsync(CancellationToken ct = default)
    {
        var all = await _unitOfWork.Disciplines.GetAllAsync(ct);
        return all.Select(static discipline => discipline.ToDto());
    }

    public Task<PagedResult<DisciplineSearchItemDto>> SearchAsync(
        string? name,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default)
    {
        return _disciplineSearchHandler.HandleAsync(
            new GetDisciplineSearchQuery(name, page, pageSize),
            ct);
    }

    public async Task<DisciplineDto?> GetByIdAsync(Guid disciplineId, CancellationToken ct = default)
    {
        var discipline = await _unitOfWork.Disciplines.GetByIdAsync(disciplineId, ct);
        return discipline is null ? null : discipline.ToDto();
    }

    public async Task<DisciplineDto> CreateAsync(CreateDisciplineDto dto, CancellationToken ct = default)
    {
        if (await _unitOfWork.Disciplines.ExistsWithNameAsync(dto.DisciplineName, ct: ct))
        {
            throw new DomainException($"A discipline named '{dto.DisciplineName}' already exists.");
        }

        var discipline = dto.ToEntity();
        _unitOfWork.Disciplines.Add(discipline);
        await _unitOfWork.SaveChangesAsync(ct);
        return discipline.ToDto();
    }

    public async Task<DisciplineDto> UpdateAsync(Guid disciplineId, UpdateDisciplineDto dto, CancellationToken ct = default)
    {
        var discipline = await _unitOfWork.Disciplines.GetByIdAsync(disciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), disciplineId);

        if (await _unitOfWork.Disciplines.ExistsWithNameAsync(dto.DisciplineName, excludeId: disciplineId, ct: ct))
        {
            throw new DomainException($"A discipline named '{dto.DisciplineName}' already exists.");
        }

        discipline.DisciplineName = dto.DisciplineName;
        discipline.Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim();
        _unitOfWork.Disciplines.Update(discipline);
        await _unitOfWork.SaveChangesAsync(ct);
        return discipline.ToDto();
    }

    public async Task DeleteAsync(Guid disciplineId, CancellationToken ct = default)
    {
        var discipline = await _unitOfWork.Disciplines.GetByIdAsync(disciplineId, ct)
            ?? throw new NotFoundException(nameof(Discipline), disciplineId);

        if (await _unitOfWork.Disciplines.IsUsedInPlanAsync(disciplineId, ct))
        {
            throw new DomainException($"Cannot delete discipline {disciplineId}: it is referenced by one or more study plans.");
        }

        _unitOfWork.Disciplines.Delete(discipline);
        await _unitOfWork.SaveChangesAsync(ct);
    }
}


using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class AcademicUnitService : IAcademicUnitService
{
    private readonly IUnitOfWork _uow;

    public AcademicUnitService(IUnitOfWork uow) => _uow = uow;

    public async Task<IEnumerable<AcademicUnitDto>> GetAllAsync(CancellationToken ct = default)
    {
        var units = await _uow.AcademicUnits.GetAllAsync(ct);
        return units.Select(Map);
    }

    public async Task<AcademicUnitDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var unit = await _uow.AcademicUnits.GetByIdAsync(id, ct);
        return unit is null ? null : Map(unit);
    }

    public async Task<AcademicUnitDto> CreateAsync(CreateAcademicUnitDto dto, CancellationToken ct = default)
    {
        var type = ParseType(dto.Type);
        var unit = new AcademicUnit { Name = dto.Name, Type = type };
        _uow.AcademicUnits.Add(unit);
        await _uow.SaveChangesAsync(ct);
        return Map(unit);
    }

    public async Task<AcademicUnitDto> UpdateAsync(int id, UpdateAcademicUnitDto dto, CancellationToken ct = default)
    {
        var unit = await _uow.AcademicUnits.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), id);

        unit.Name = dto.Name;
        unit.Type = ParseType(dto.Type);
        _uow.AcademicUnits.Update(unit);
        await _uow.SaveChangesAsync(ct);
        return Map(unit);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var unit = await _uow.AcademicUnits.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), id);

        if (await _uow.AcademicUnits.HasDepartmentsAsync(id, ct))
            throw new DomainException($"Cannot delete academic unit {id}: it has departments. Remove them first.");

        _uow.AcademicUnits.Remove(unit);
        await _uow.SaveChangesAsync(ct);
    }

    private static AcademicUnitType ParseType(string value)
    {
        if (!Enum.TryParse<AcademicUnitType>(value, ignoreCase: true, out var result))
            throw new DomainException($"Invalid academic unit type '{value}'. Valid: Faculty, Institute.");
        return result;
    }

    private static AcademicUnitDto Map(AcademicUnit u) =>
        new(u.AcademicUnitId, u.Name, u.Type.ToString(),
            u.Departments.Select(d => new DepartmentSummaryDto(d.DepartmentId, d.Name)));
}

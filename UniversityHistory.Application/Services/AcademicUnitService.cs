using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class AcademicUnitService : IAcademicUnitService
{
    private readonly IUnitOfWork _unitOfWork;

    public AcademicUnitService(IUnitOfWork uow) => _unitOfWork = uow;

    public async Task<IEnumerable<AcademicUnitDto>> GetAllAsync(CancellationToken ct = default)
    {
        var units = await _unitOfWork.AcademicUnits.GetAllAsync(ct);
        return units.Select(Map);
    }

    public async Task<AcademicUnitDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var unit = await _unitOfWork.AcademicUnits.GetByIdAsync(id, ct);
        return unit is null ? null : Map(unit);
    }

    public async Task<AcademicUnitDto> CreateAsync(CreateAcademicUnitDto dto, CancellationToken ct = default)
    {
        var type = ParseType(dto.Type);
        var unit = new AcademicUnit { Name = dto.Name, Type = type };
        _unitOfWork.AcademicUnits.Add(unit);
        await _unitOfWork.SaveChangesAsync(ct);
        return Map(unit);
    }

    public async Task<AcademicUnitDto> UpdateAsync(Guid id, UpdateAcademicUnitDto dto, CancellationToken ct = default)
    {
        var unit = await _unitOfWork.AcademicUnits.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), id);

        unit.Name = dto.Name;
        unit.Type = ParseType(dto.Type);
        _unitOfWork.AcademicUnits.Update(unit);
        await _unitOfWork.SaveChangesAsync(ct);
        return Map(unit);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var unit = await _unitOfWork.AcademicUnits.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), id);

        if (await _unitOfWork.AcademicUnits.HasDepartmentsAsync(id, ct))
            throw new DomainException($"Cannot delete academic unit {id}: it has departments. Remove them first.");

        _unitOfWork.AcademicUnits.Remove(unit);
        await _unitOfWork.SaveChangesAsync(ct);
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


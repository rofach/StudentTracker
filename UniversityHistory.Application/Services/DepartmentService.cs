using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _uow;

    public DepartmentService(IUnitOfWork uow) => _uow = uow;

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken ct = default)
    {
        var depts = await _uow.Departments.GetAllAsync(ct);
        return depts.Select(Map);
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var dept = await _uow.Departments.GetByIdAsync(id, ct);
        return dept is null ? null : Map(dept);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken ct = default)
    {
        _ = await _uow.AcademicUnits.GetByIdAsync(dto.AcademicUnitId, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), dto.AcademicUnitId);

        var dept = new Department { AcademicUnitId = dto.AcademicUnitId, Name = dto.Name };
        _uow.Departments.Add(dept);
        await _uow.SaveChangesAsync(ct);

        var created = await _uow.Departments.GetByIdAsync(dept.DepartmentId, ct)!;
        return Map(created!);
    }

    public async Task<DepartmentDto> UpdateAsync(int id, UpdateDepartmentDto dto, CancellationToken ct = default)
    {
        var dept = await _uow.Departments.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Department), id);

        dept.Name = dto.Name;
        _uow.Departments.Update(dept);
        await _uow.SaveChangesAsync(ct);
        return Map(dept);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var dept = await _uow.Departments.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Department), id);

        if (await _uow.Departments.HasGroupsAsync(id, ct))
            throw new DomainException($"Cannot delete department {id}: it has study groups assigned to it.");

        _uow.Departments.Remove(dept);
        await _uow.SaveChangesAsync(ct);
    }

    private static DepartmentDto Map(Department d) =>
        new(d.DepartmentId, d.AcademicUnitId, d.Name,
            d.AcademicUnit.Name, d.AcademicUnit.Type.ToString());
}

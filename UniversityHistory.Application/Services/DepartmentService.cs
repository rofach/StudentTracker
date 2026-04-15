using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentService(IUnitOfWork uow) => _unitOfWork = uow;

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken ct = default)
    {
        var depts = await _unitOfWork.Departments.GetAllAsync(ct);
        return depts.Select(Map);
    }

    public async Task<DepartmentDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var dept = await _unitOfWork.Departments.GetByIdAsync(id, ct);
        return dept is null ? null : Map(dept);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken ct = default)
    {
        _ = await _unitOfWork.AcademicUnits.GetByIdAsync(dto.AcademicUnitId, ct)
            ?? throw new NotFoundException(nameof(AcademicUnit), dto.AcademicUnitId);

        var dept = new Department { AcademicUnitId = dto.AcademicUnitId, Name = dto.Name };
        _unitOfWork.Departments.Add(dept);
        await _unitOfWork.SaveChangesAsync(ct);

        var created = await _unitOfWork.Departments.GetByIdAsync(dept.DepartmentId, ct)!;
        return Map(created!);
    }

    public async Task<DepartmentDto> UpdateAsync(Guid id, UpdateDepartmentDto dto, CancellationToken ct = default)
    {
        var dept = await _unitOfWork.Departments.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Department), id);

        dept.Name = dto.Name;
        _unitOfWork.Departments.Update(dept);
        await _unitOfWork.SaveChangesAsync(ct);
        return Map(dept);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var dept = await _unitOfWork.Departments.GetByIdAsync(id, ct)
            ?? throw new NotFoundException(nameof(Department), id);

        if (await _unitOfWork.Departments.HasGroupsAsync(id, ct))
            throw new DomainException($"Cannot delete department {id}: it has study groups assigned to it.");

        _unitOfWork.Departments.Remove(dept);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    private static DepartmentDto Map(Department d) =>
        new(d.DepartmentId, d.AcademicUnitId, d.Name,
            d.AcademicUnit.Name, d.AcademicUnit.Type.ToString());
}


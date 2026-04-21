using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class DisciplineMappingExtensions
{
    public static Discipline ToEntity(this CreateDisciplineDto dto)
    {
        return new Discipline
        {
            DisciplineName = dto.DisciplineName,
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim()
        };
    }

    public static DisciplineDto ToDto(this Discipline discipline)
    {
        return new DisciplineDto(discipline.DisciplineId, discipline.DisciplineName, discipline.Description);
    }
}


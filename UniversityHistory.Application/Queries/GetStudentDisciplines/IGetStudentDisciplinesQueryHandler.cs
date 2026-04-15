using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetStudentDisciplines;

public interface IGetStudentDisciplinesQueryHandler
{
    Task<IReadOnlyList<StudentDisciplineOptionDto>> HandleAsync(
        GetStudentDisciplinesQuery query,
        CancellationToken ct = default);
}


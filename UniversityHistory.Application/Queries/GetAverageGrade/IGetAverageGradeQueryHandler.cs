using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetAverageGrade;

public interface IGetAverageGradeQueryHandler
{
    Task<AverageGradeDto> HandleAsync(GetAverageGradeQuery query, CancellationToken ct = default);
}

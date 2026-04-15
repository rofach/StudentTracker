using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetStudentSearch;

public interface IGetStudentSearchQueryHandler
{
    Task<PagedResult<StudentDto>> HandleAsync(GetStudentSearchQuery query, CancellationToken ct = default);
}


using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetStudentsInGroup;

public interface IGetStudentsInGroupQueryHandler
{
    Task<IEnumerable<GroupStudentDto>> HandleAsync(GetStudentsInGroupQuery query, CancellationToken ct = default);
}

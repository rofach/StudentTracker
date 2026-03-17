using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetClassmates;

public interface IGetClassmatesQueryHandler
{
    Task<IEnumerable<ClassmateDto>> HandleAsync(GetClassmatesQuery query, CancellationToken ct = default);
}

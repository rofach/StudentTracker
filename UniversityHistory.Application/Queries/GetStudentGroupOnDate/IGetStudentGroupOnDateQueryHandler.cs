using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetStudentGroupOnDate;

public interface IGetStudentGroupOnDateQueryHandler
{
    Task<StudentCurrentGroupDto?> HandleAsync(GetStudentGroupOnDateQuery query, CancellationToken ct = default);
}

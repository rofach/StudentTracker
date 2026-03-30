using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IGradeService
{
    Task<PagedResult<GradeDto>> GetGradesAsync(int studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<AverageGradeDto> GetAverageGradeAsync(int studentId, int? semesterNo, int? disciplineId, int? academicYearStart, CancellationToken ct = default);
}

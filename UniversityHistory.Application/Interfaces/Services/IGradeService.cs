using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IGradeService
{
    Task<PagedResult<GradeDto>> GetGradesAsync(Guid studentId, int page = 1, int pageSize = 20, CancellationToken ct = default);
    Task<AverageGradeDto> GetAverageGradeAsync(Guid studentId, int? semesterNo, Guid? disciplineId, int? academicYearStart, CancellationToken ct = default);
    Task<IReadOnlyList<StudentDisciplineOptionDto>> GetStudentDisciplinesAsync(Guid studentId, CancellationToken ct = default);
}


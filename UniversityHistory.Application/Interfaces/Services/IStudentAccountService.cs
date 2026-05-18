using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Interfaces.Services;

public interface IStudentAccountService
{
    Task<StudentCreatedResultDto> CreateStudentAsync(StudentCreateDto dto, CancellationToken ct = default);
    Task<StudentCreatedResultDto> CreateTransferredStudentAsync(CreateTransferredStudentDto dto, CancellationToken ct = default);
    Task<StudentDto> UpdateStudentAsync(Guid studentId, StudentUpdateDto dto, CancellationToken ct = default);
    Task<StudentAccountPasswordDto> ResetPasswordAsync(Guid studentId, ResetStudentPasswordDto dto, CancellationToken ct = default);
}

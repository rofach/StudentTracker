using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Domain.Interfaces.Repositories;

public interface IStudentGroupTransferRepository
{
    StudentGroupTransfer Add(StudentGroupTransfer transfer);
    void AddDifferenceItems(IEnumerable<AcademicDifferenceItem> items);
    Task<StudentGroupTransfer?> GetByIdAsync(Guid transferId, CancellationToken ct = default);
    Task<IEnumerable<StudentGroupTransfer>> GetByStudentIdAsync(Guid studentId, CancellationToken ct = default);
    Task<AcademicDifferenceItem?> GetDifferenceItemByIdAsync(Guid differenceItemId, CancellationToken ct = default);
    void UpdateDifferenceItem(AcademicDifferenceItem item);
}

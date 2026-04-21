namespace UniversityHistory.Application.DTOs;

public record ActiveAcademicDifferenceDto(
    Guid DifferenceItemId,
    Guid TransferId,
    Guid StudentId,
    string StudentName,
    string OldGroupCode,
    string NewGroupCode,
    DateOnly TransferDate,
    string DisciplineName,
    int SemesterNo,
    string Status,
    string? Notes
);

public record InternalTransferJournalItemDto(
    Guid TransferId,
    Guid StudentId,
    string StudentName,
    string OldGroupCode,
    string NewGroupCode,
    DateOnly TransferDate,
    string Reason,
    int DifferenceItemsTotal,
    int DifferenceItemsPending,
    int DifferenceItemsCompleted,
    int DifferenceItemsWaived
);

public record DisciplineSearchItemDto(
    Guid DisciplineId,
    string DisciplineName,
    int PlanUsageCount
);

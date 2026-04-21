namespace UniversityHistory.Application.Queries.GetInternalTransferJournal;

public record GetInternalTransferJournalQuery(
    string? StudentName,
    DateOnly? DateFrom,
    DateOnly? DateTo,
    bool OnlyWithPendingDifference,
    int Page,
    int PageSize
);

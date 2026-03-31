namespace UniversityHistory.Domain.Common;

public record PagedData<T>(
    IReadOnlyList<T> Items,
    int TotalCount
);

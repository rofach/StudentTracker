using UniversityHistory.Application.DTOs;

namespace UniversityHistory.Application.Queries.GetInternalTransferJournal;

public interface IGetInternalTransferJournalQueryHandler
{
    Task<PagedResult<InternalTransferJournalItemDto>> HandleAsync(
        GetInternalTransferJournalQuery query,
        CancellationToken ct = default);
}

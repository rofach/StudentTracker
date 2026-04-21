using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetDisciplineSearch;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetDisciplineSearchQueryHandler : IGetDisciplineSearchQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetDisciplineSearchQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<PagedResult<DisciplineSearchItemDto>> HandleAsync(
        GetDisciplineSearchQuery query,
        CancellationToken ct = default)
    {
        var name = string.IsNullOrWhiteSpace(query.Name) ? null : query.Name.Trim();

        var rawQuery = _db.Database.SqlQuery<DisciplineSearchItemDto>($"""
            SELECT
                d.discipline_id                                 AS DisciplineId,
                d.discipline_name                               AS DisciplineName,
                d.description                                   AS Description,
                COUNT(pd.plan_discipline_id)                    AS PlanUsageCount
            FROM Discipline d
            LEFT JOIN Plan_Disciplines pd
                ON pd.discipline_id = d.discipline_id
            WHERE ({name} IS NULL OR d.discipline_name LIKE N'%' + {name} + N'%')
            GROUP BY d.discipline_id, d.discipline_name, d.description
            """);

        var totalCount = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderBy(item => item.DisciplineName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<DisciplineSearchItemDto>(items, query.Page, query.PageSize, totalCount);
    }
}

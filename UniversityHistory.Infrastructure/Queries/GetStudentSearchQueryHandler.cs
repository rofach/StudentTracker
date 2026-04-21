using Microsoft.EntityFrameworkCore;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Queries.GetStudentSearch;
using UniversityHistory.Infrastructure.Data;

namespace UniversityHistory.Infrastructure.Queries;

public class GetStudentSearchQueryHandler : IGetStudentSearchQueryHandler
{
    private readonly UniversityDbContext _db;

    public GetStudentSearchQueryHandler(UniversityDbContext db) => _db = db;

    public async Task<PagedResult<StudentDto>> HandleAsync(
        GetStudentSearchQuery query,
        CancellationToken ct = default)
    {
        var fullName = string.IsNullOrWhiteSpace(query.FullName)
            ? null
            : query.FullName.Trim();
        var email = string.IsNullOrWhiteSpace(query.Email)
            ? null
            : query.Email.Trim();
        var status = string.IsNullOrWhiteSpace(query.Status)
            ? null
            : query.Status.Trim();
        var nameTokens = fullName?
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Take(3)
            .ToArray() ?? [];

        var token1 = nameTokens.ElementAtOrDefault(0);
        var token2 = nameTokens.ElementAtOrDefault(1);
        var token3 = nameTokens.ElementAtOrDefault(2);

        var rawQuery = _db.Database.SqlQuery<StudentDto>($"""
            SELECT
                s.student_id    AS StudentId,
                s.first_name    AS FirstName,
                s.last_name     AS LastName,
                s.patronymic    AS Patronymic,
                s.birth_date    AS BirthDate,
                s.email         AS Email,
                s.phone         AS Phone,
                s.status        AS Status
            FROM Student s
            WHERE ({status} IS NULL OR s.status = {status})
              AND (
                    {token1} IS NULL
                    OR s.last_name LIKE N'%' + {token1} + N'%'
                    OR s.first_name LIKE N'%' + {token1} + N'%'
                    OR ISNULL(s.patronymic, N'') LIKE N'%' + {token1} + N'%'
                  )
              AND (
                    {token2} IS NULL
                    OR s.last_name LIKE N'%' + {token2} + N'%'
                    OR s.first_name LIKE N'%' + {token2} + N'%'
                    OR ISNULL(s.patronymic, N'') LIKE N'%' + {token2} + N'%'
                  )
              AND (
                    {token3} IS NULL
                    OR s.last_name LIKE N'%' + {token3} + N'%'
                    OR s.first_name LIKE N'%' + {token3} + N'%'
                    OR ISNULL(s.patronymic, N'') LIKE N'%' + {token3} + N'%'
                  )
              AND (
                    {email} IS NULL
                    OR s.email LIKE N'%' + {email} + N'%'
                  )
            """);

        var count = await rawQuery.CountAsync(ct);
        var items = await rawQuery
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<StudentDto>(items, query.Page, query.PageSize, count);
    }
}


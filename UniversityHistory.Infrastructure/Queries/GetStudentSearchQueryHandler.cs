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
                    {fullName} IS NULL
                    OR CONCAT(s.last_name, N' ', s.first_name) LIKE N'%' + {fullName} + N'%'
                    OR CONCAT(s.first_name, N' ', s.last_name) LIKE N'%' + {fullName} + N'%'
                    OR CONCAT(s.last_name, N' ', s.first_name, N' ', ISNULL(s.patronymic, N'')) LIKE N'%' + {fullName} + N'%'
                    OR CONCAT(s.first_name, N' ', ISNULL(s.patronymic, N''), N' ', s.last_name) LIKE N'%' + {fullName} + N'%'
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


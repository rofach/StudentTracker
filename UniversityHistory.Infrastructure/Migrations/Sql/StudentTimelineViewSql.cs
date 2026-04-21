namespace UniversityHistory.Infrastructure.Migrations.Sql;

internal static class StudentTimelineViewSql
{
    public const string Create = """
        CREATE VIEW vw_StudentTimeline
        AS
        WITH transfer_diff AS (
            SELECT
                adi.transfer_id,
                COUNT(*) AS total_count,
                SUM(CASE WHEN adi.status = 'Pending' THEN 1 ELSE 0 END) AS pending_count,
                SUM(CASE WHEN adi.status = 'Completed' THEN 1 ELSE 0 END) AS completed_count,
                SUM(CASE WHEN adi.status = 'Waived' THEN 1 ELSE 0 END) AS waived_count
            FROM Academic_Difference_Item adi
            GROUP BY adi.transfer_id
        )
        SELECT
            timeline.StudentId,
            timeline.EventType,
            timeline.Description,
            timeline.DateFrom,
            timeline.DateTo,
            timeline.GroupCode,
            timeline.DepartmentName,
            timeline.AcademicUnitName,
            timeline.AcademicUnitType,
            timeline.SortPriority,
            timeline.EventKey
        FROM (
            SELECT
                e.student_id AS StudentId,
                'EnrollmentStart' AS EventType,
                CONCAT('Enrollment started in group ', g.group_code, ' (', e.reason_start, ')') AS Description,
                e.date_from AS DateFrom,
                CAST(NULL AS date) AS DateTo,
                g.group_code AS GroupCode,
                d.name AS DepartmentName,
                au.name AS AcademicUnitName,
                au.type AS AcademicUnitType,
                10 AS SortPriority,
                CONCAT('EnrollmentStart:', CONVERT(nvarchar(36), e.enrollment_id)) AS EventKey
            FROM Student_Group_Enrollment e
            JOIN Study_Group g ON g.group_id = e.group_id
            JOIN Department d ON d.department_id = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                e.student_id AS StudentId,
                'EnrollmentEnd',
                CONCAT(
                    'Enrollment ended in group ',
                    g.group_code,
                    CASE WHEN e.reason_end IS NOT NULL
                        THEN CONCAT(' (', e.reason_end, ')')
                        ELSE ''
                    END
                ),
                e.date_to,
                CAST(NULL AS date),
                g.group_code,
                d.name,
                au.name,
                au.type,
                20,
                CONCAT('EnrollmentEnd:', CONVERT(nvarchar(36), e.enrollment_id))
            FROM Student_Group_Enrollment e
            JOIN Study_Group g ON g.group_id = e.group_id
            JOIN Department d ON d.department_id = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id
            WHERE e.date_to IS NOT NULL

            UNION ALL

            SELECT
                e.student_id AS StudentId,
                'AcademicLeaveStart',
                CONCAT('Academic leave started: ', ISNULL(al.reason, 'No reason provided')),
                al.start_date,
                CAST(NULL AS date),
                g.group_code,
                d.name,
                au.name,
                au.type,
                30,
                CONCAT('AcademicLeaveStart:', CONVERT(nvarchar(36), al.leave_id))
            FROM Academic_Leave al
            JOIN Student_Group_Enrollment e ON e.enrollment_id = al.enrollment_id
            JOIN Study_Group g ON g.group_id = e.group_id
            JOIN Department d ON d.department_id = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                e.student_id AS StudentId,
                'AcademicLeaveEnd',
                CONCAT(
                    'Academic leave ended',
                    CASE WHEN al.return_reason IS NOT NULL
                        THEN CONCAT(': ', al.return_reason)
                        ELSE ''
                    END
                ),
                al.end_date,
                CAST(NULL AS date),
                g.group_code,
                d.name,
                au.name,
                au.type,
                40,
                CONCAT('AcademicLeaveEnd:', CONVERT(nvarchar(36), al.leave_id))
            FROM Academic_Leave al
            JOIN Student_Group_Enrollment e ON e.enrollment_id = al.enrollment_id
            JOIN Study_Group g ON g.group_id = e.group_id
            JOIN Department d ON d.department_id = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id
            WHERE al.end_date IS NOT NULL

            UNION ALL

            SELECT
                e.student_id AS StudentId,
                'SubgroupChange',
                CONCAT('Subgroup changed to ', sg.subgroup_name, ' (', se.reason, ')'),
                se.date_from,
                CAST(NULL AS date),
                g.group_code,
                d.name,
                au.name,
                au.type,
                50,
                CONCAT('SubgroupChange:', CONVERT(nvarchar(36), se.subgroup_enrollment_id))
            FROM Student_Subgroup_Enrollment se
            JOIN Student_Group_Enrollment e ON e.enrollment_id = se.enrollment_id
            JOIN Subgroup sg ON sg.subgroup_id = se.subgroup_id
            JOIN Study_Group g ON g.group_id = e.group_id
            JOIN Department d ON d.department_id = g.department_id
            JOIN Academic_Unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                old_e.student_id AS StudentId,
                'GroupTransfer',
                CONCAT(
                    'Transferred from group ',
                    old_g.group_code,
                    ' to ',
                    new_g.group_code,
                    ' (',
                    t.reason,
                    ')',
                    CASE WHEN ISNULL(diff.total_count, 0) > 0
                        THEN CONCAT(
                            '. Academic difference: pending=',
                            ISNULL(diff.pending_count, 0),
                            ', completed=',
                            ISNULL(diff.completed_count, 0),
                            ', waived=',
                            ISNULL(diff.waived_count, 0)
                        )
                        ELSE ''
                    END
                ),
                t.transfer_date,
                CAST(NULL AS date),
                new_g.group_code,
                new_d.name,
                new_au.name,
                new_au.type,
                60,
                CONCAT('GroupTransfer:', CONVERT(nvarchar(36), t.transfer_id))
            FROM Student_Group_Transfer t
            JOIN Student_Group_Enrollment old_e ON old_e.enrollment_id = t.old_enrollment_id
            JOIN Student_Group_Enrollment new_e ON new_e.enrollment_id = t.new_enrollment_id
            JOIN Study_Group old_g ON old_g.group_id = old_e.group_id
            JOIN Study_Group new_g ON new_g.group_id = new_e.group_id
            JOIN Department new_d ON new_d.department_id = new_g.department_id
            JOIN Academic_Unit new_au ON new_au.academic_unit_id = new_d.academic_unit_id
            LEFT JOIN transfer_diff diff ON diff.transfer_id = t.transfer_id

            UNION ALL

            SELECT
                et.student_id AS StudentId,
                'ExternalTransfer',
                CONCAT(
                    et.transfer_type,
                    ' transfer ',
                    i.institution_name,
                    CASE WHEN et.notes IS NOT NULL
                        THEN CONCAT(' (', et.notes, ')')
                        ELSE ''
                    END
                ),
                et.transfer_date,
                CAST(NULL AS date),
                CAST(NULL AS nvarchar(20)),
                CAST(NULL AS nvarchar(200)),
                CAST(NULL AS nvarchar(200)),
                CAST(NULL AS nvarchar(20)),
                70,
                CONCAT('ExternalTransfer:', CONVERT(nvarchar(36), et.transfer_id))
            FROM External_Transfers et
            JOIN Institution i ON i.institution_id = et.institution_id
        ) AS timeline;
        """;

    public const string Drop = """
        DROP VIEW IF EXISTS vw_StudentTimeline;
        """;
}

namespace UniversityHistory.Infrastructure.Migrations.Sql;

internal static class StudentTimelineViewSql
{
    public const string Create = """
        CREATE VIEW vw_student_timeline
        AS
        WITH transfer_diff AS (
            SELECT
                adi.transfer_id,
                COUNT(*) AS total_count,
                SUM(CASE WHEN adi.status = 'Pending' THEN 1 ELSE 0 END) AS pending_count,
                SUM(CASE WHEN adi.status = 'Completed' THEN 1 ELSE 0 END) AS completed_count,
                SUM(CASE WHEN adi.status = 'Waived' THEN 1 ELSE 0 END) AS waived_count
            FROM academic_difference_item adi
            GROUP BY adi.transfer_id
        )
        SELECT
            timeline.student_id,
            timeline.event_type,
            timeline.description,
            timeline.date_from,
            timeline.date_to,
            timeline.group_code,
            timeline.department_name,
            timeline.academic_unit_name,
            timeline.academic_unit_type,
            timeline.sort_priority,
            timeline.event_key
        FROM (
            SELECT
                e.student_id,
                'EnrollmentStart' AS event_type,
                CONCAT('Enrollment started in group ', g.group_code, ' (', e.reason_start, ')') AS description,
                e.date_from,
                CAST(NULL AS date) AS date_to,
                g.group_code,
                d.name AS department_name,
                au.name AS academic_unit_name,
                au.type AS academic_unit_type,
                10 AS sort_priority,
                CONCAT('EnrollmentStart:', CONVERT(nvarchar(36), e.enrollment_id)) AS event_key
            FROM student_group_enrollment e
            JOIN study_group g ON g.group_id = e.group_id
            JOIN department d ON d.department_id = g.department_id
            JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                e.student_id,
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
            FROM student_group_enrollment e
            JOIN study_group g ON g.group_id = e.group_id
            JOIN department d ON d.department_id = g.department_id
            JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id
            WHERE e.date_to IS NOT NULL

            UNION ALL

            SELECT
                e.student_id,
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
            FROM academic_leave al
            JOIN student_group_enrollment e ON e.enrollment_id = al.enrollment_id
            JOIN study_group g ON g.group_id = e.group_id
            JOIN department d ON d.department_id = g.department_id
            JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                e.student_id,
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
            FROM academic_leave al
            JOIN student_group_enrollment e ON e.enrollment_id = al.enrollment_id
            JOIN study_group g ON g.group_id = e.group_id
            JOIN department d ON d.department_id = g.department_id
            JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id
            WHERE al.end_date IS NOT NULL

            UNION ALL

            SELECT
                e.student_id,
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
            FROM student_subgroup_enrollment se
            JOIN student_group_enrollment e ON e.enrollment_id = se.enrollment_id
            JOIN subgroup sg ON sg.subgroup_id = se.subgroup_id
            JOIN study_group g ON g.group_id = e.group_id
            JOIN department d ON d.department_id = g.department_id
            JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id

            UNION ALL

            SELECT
                old_e.student_id,
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
            FROM student_group_transfer t
            JOIN student_group_enrollment old_e ON old_e.enrollment_id = t.old_enrollment_id
            JOIN student_group_enrollment new_e ON new_e.enrollment_id = t.new_enrollment_id
            JOIN study_group old_g ON old_g.group_id = old_e.group_id
            JOIN study_group new_g ON new_g.group_id = new_e.group_id
            JOIN department new_d ON new_d.department_id = new_g.department_id
            JOIN academic_unit new_au ON new_au.academic_unit_id = new_d.academic_unit_id
            LEFT JOIN transfer_diff diff ON diff.transfer_id = t.transfer_id

            UNION ALL

            SELECT
                et.student_id,
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
            FROM external_transfers et
            JOIN institution i ON i.institution_id = et.institution_id
        ) AS timeline;
        """;

    public const string Drop = """
        DROP VIEW IF EXISTS vw_student_timeline;
        """;
}

DECLARE @studentId UNIQUEIDENTIFIER = N'00000001-0000-0000-0000-000000000000';

SELECT DISTINCT
    ce.course_enrollment_id                         AS course_enrollment_id,
    pd.discipline_id                                AS discipline_id,
    d.discipline_name                               AS discipline_name,
    pd.semester_no                                  AS semester_no,
    CAST(ce.academic_year_start AS NVARCHAR(4))
        + N'/' + CAST(ce.academic_year_start + 1 AS NVARCHAR(4)) AS academic_year_label,
    CASE
        WHEN EXISTS (
            SELECT 1
            FROM grade_record gr
            JOIN student_course_enrollment ce_inner
                ON ce_inner.course_enrollment_id = gr.course_enrollment_id
            JOIN plan_disciplines pd_inner
                ON pd_inner.plan_discipline_id = ce_inner.plan_discipline_id
            JOIN student_group_enrollment e_inner
                ON e_inner.enrollment_id = ce_inner.enrollment_id
            WHERE e_inner.student_id    = @studentId
              AND pd_inner.discipline_id = pd.discipline_id
        ) THEN 1
        ELSE 0
    END                                             AS has_grade
FROM student_course_enrollment ce
JOIN student_group_enrollment e ON e.enrollment_id          = ce.enrollment_id
JOIN plan_disciplines        pd ON pd.plan_discipline_id    = ce.plan_discipline_id
JOIN discipline               d ON d.discipline_id          = pd.discipline_id
WHERE e.student_id = @studentId
ORDER BY pd.semester_no, d.discipline_name;

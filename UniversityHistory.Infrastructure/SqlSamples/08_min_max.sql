SELECT
    MIN(avg_per_student.avg_grade) AS min_avg_grade,
    MAX(avg_per_student.avg_grade) AS max_avg_grade
FROM (
    SELECT
        e.student_id,
        AVG(TRY_CAST(gr.grade_value AS DECIMAL(10,2))) AS avg_grade
    FROM grade_record gr
    JOIN student_course_enrollment ce ON ce.course_enrollment_id = gr.course_enrollment_id
    JOIN student_group_enrollment  e  ON e.enrollment_id         = ce.enrollment_id
    WHERE TRY_CAST(gr.grade_value AS DECIMAL(10,2)) IS NOT NULL
    GROUP BY e.student_id
) avg_per_student;
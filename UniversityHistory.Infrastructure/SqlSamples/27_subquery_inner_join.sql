DECLARE @student_id UNIQUEIDENTIFIER = '0000000D-0000-0000-0000-000000000000';
DECLARE @current_date DATE = CAST(GETDATE() AS date);

SELECT DISTINCT
    d.discipline_name   AS discipline_name,
    pd.semester_no      AS semester_no,
    pd.credits          AS credits
FROM student_course_enrollment ce
JOIN student_group_enrollment e ON e.enrollment_id       = ce.enrollment_id
JOIN plan_disciplines        pd ON pd.plan_discipline_id = ce.plan_discipline_id
JOIN discipline               d ON d.discipline_id       = pd.discipline_id
INNER JOIN (
    SELECT TOP 1 gpa.plan_id
    FROM group_plan_assignment gpa
    JOIN student_group_enrollment sge ON sge.group_id = gpa.group_id
    WHERE sge.student_id = @student_id
      AND sge.date_to IS NULL
      AND gpa.date_from <= @current_date
      AND (gpa.date_to IS NULL OR gpa.date_to >= @current_date)
    ORDER BY gpa.date_from DESC
) current_plan ON current_plan.plan_id = pd.plan_id
WHERE e.student_id = @student_id
ORDER BY pd.semester_no, d.discipline_name;

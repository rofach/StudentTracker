DECLARE @studentId    UNIQUEIDENTIFIER = N'0000000B-0000-0000-0000-000000000000';
DECLARE @semesterNo   INT              = 2;
DECLARE @academicYear INT              = 2025;

SELECT
    AVG(CAST(gr.grade_value AS DECIMAL(10,2)))  AS average,
    COUNT(gr.grade_id)                              AS grade_count
FROM grade_record gr
JOIN student_course_enrollment ce  ON ce.course_enrollment_id = gr.course_enrollment_id
JOIN student_group_enrollment  e   ON e.enrollment_id         = ce.enrollment_id
JOIN plan_disciplines          pd  ON pd.plan_discipline_id   = ce.plan_discipline_id
WHERE e.student_id = @studentId
  AND (@semesterNo   IS NULL OR pd.semester_no = @semesterNo)
  AND (@academicYear IS NULL OR ce.academic_year_start = @academicYear)
  AND gr.grade_value IS NOT NULL;

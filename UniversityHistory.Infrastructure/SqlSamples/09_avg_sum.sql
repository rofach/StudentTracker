DECLARE @studentId    UNIQUEIDENTIFIER = N'00000006-0000-0000-0000-000000000000'; 
DECLARE @semesterNo   INT  = NULL;
DECLARE @disciplineId UNIQUEIDENTIFIER = NULL; 
DECLARE @academicYear INT  = NULL;

SELECT
    AVG(CAST(gr.grade_value AS DECIMAL(10,2)))  AS average,
    COUNT(gr.grade_id)                              AS grade_count
FROM grade_record gr
JOIN student_course_enrollment ce  ON ce.course_enrollment_id = gr.course_enrollment_id
JOIN student_group_enrollment  e   ON e.enrollment_id         = ce.enrollment_id
JOIN plan_disciplines          pd  ON pd.plan_discipline_id   = ce.plan_discipline_id
WHERE e.student_id                    = @studentId
  AND (@semesterNo   IS NULL OR pd.semester_no         = @semesterNo)
  AND (@disciplineId IS NULL OR pd.discipline_id       = @disciplineId)
  AND (@academicYear IS NULL OR ce.academic_year_start = @academicYear)
  AND gr.grade_value IS NOT NULL;

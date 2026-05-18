SELECT DISTINCT
    s.student_id    AS StudentId,
    s.first_name    AS FirstName,
    s.last_name     AS LastName,
    s.status        AS Status
FROM Student s
JOIN Student_Group_Enrollment e ON e.student_id = s.student_id
WHERE (
    SELECT AVG(TRY_CAST(gr.grade_value AS DECIMAL(10,2)))
    FROM Grade_Record gr
    JOIN Student_Course_Enrollment ce ON ce.course_enrollment_id = gr.course_enrollment_id
    WHERE ce.enrollment_id = e.enrollment_id
      AND TRY_CAST(gr.grade_value AS DECIMAL(10,2)) IS NOT NULL
) > 85
ORDER BY s.last_name, s.first_name;

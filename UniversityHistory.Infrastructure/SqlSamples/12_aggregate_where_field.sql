SELECT
    e.group_id,
    g.group_code,
    COUNT(e.enrollment_id) AS ActiveStudentCount
FROM Student_Group_Enrollment e
JOIN Student     s ON s.student_id = e.student_id
JOIN Study_Group g ON g.group_id   = e.group_id
WHERE s.status = N'Active'
  AND e.date_to IS NULL
GROUP BY e.group_id, g.group_code
ORDER BY ActiveStudentCount DESC;

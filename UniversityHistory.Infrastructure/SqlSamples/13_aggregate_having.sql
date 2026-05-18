SELECT
    e.group_id,
    g.group_code,
    COUNT(e.enrollment_id) AS CurrentStudentCount
FROM Student_Group_Enrollment e
JOIN Study_Group g ON g.group_id = e.group_id
WHERE e.date_to IS NULL
GROUP BY e.group_id, g.group_code
HAVING COUNT(e.enrollment_id) >= 5
ORDER BY CurrentStudentCount DESC;

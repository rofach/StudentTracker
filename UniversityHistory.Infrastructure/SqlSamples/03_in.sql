SELECT
    student_id,
    first_name,
    last_name,
    status
FROM student
WHERE status IN (N'Active', N'Graduated');

SELECT
    student_id,
    first_name,
    last_name,
    birth_date,
    status
FROM student
WHERE status    = N'Active'
  AND birth_date >= '2006-01-01';

SELECT
    student_id,
    first_name,
    last_name,
    status
FROM student
WHERE status = N'Expelled'
   OR status = N'Graduated';



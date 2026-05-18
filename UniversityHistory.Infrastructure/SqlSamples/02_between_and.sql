SELECT
    student_id,
    first_name,
    last_name,
    birth_date,
    status
FROM student
WHERE birth_date BETWEEN '1998-01-01' AND '2005-12-31';
    
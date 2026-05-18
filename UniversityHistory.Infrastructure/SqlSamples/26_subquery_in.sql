SELECT
    s.student_id    AS student_id,
    s.first_name    AS first_name,
    s.last_name     AS last_name,
    s.status        AS status
FROM student s
WHERE s.student_id IN (
    SELECT e.student_id
    FROM student_group_enrollment e
    JOIN study_group   g  ON g.group_id          = e.group_id
    JOIN department    d  ON d.department_id      = g.department_id
    JOIN academic_unit au ON au.academic_unit_id  = d.academic_unit_id
    WHERE au.name LIKE N'%Факультет Обчислювальної%'
      AND e.date_to IS NULL
)
ORDER BY s.last_name, s.first_name;

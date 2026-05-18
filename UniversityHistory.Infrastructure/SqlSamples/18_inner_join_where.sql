DECLARE @groupId UNIQUEIDENTIFIER = N'00000004-0000-0000-0000-000000000000';
DECLARE @date    DATE             = CAST(GETDATE() AS date);

SELECT
    e.enrollment_id   AS enrollment_id,
    e.student_id      AS student_id,
    s.first_name      AS first_name,
    s.last_name       AS last_name,
    s.email           AS email,
    se.subgroup_id   AS subgroup_id,
    sg.subgroup_name AS subgroup_name,
    e.date_from       AS date_from,
    e.date_to         AS date_to
FROM student_group_enrollment e
JOIN student s ON s.student_id = e.student_id
LEFT JOIN student_subgroup_enrollment se 
    ON se.enrollment_id = e.enrollment_id
   AND se.date_from <= @date
   AND (se.date_to IS NULL OR se.date_to >= @date)
LEFT JOIN subgroup sg ON sg.subgroup_id = se.subgroup_id
WHERE e.group_id  = @groupId
  AND e.date_from <= @date
  AND (e.date_to IS NULL OR e.date_to >= @date)
ORDER BY s.last_name, s.first_name;
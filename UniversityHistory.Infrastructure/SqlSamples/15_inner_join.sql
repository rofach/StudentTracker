DECLARE @date DATE = '2024-09-01';

SELECT
    g.group_id    AS group_id,
    g.group_code  AS group_code,
    d.name        AS department_name,
    au.name       AS academic_unit_name,
    au.type       AS academic_unit_type,
    g.date_created AS date_created,
    g.date_closed AS date_closed,
    CASE WHEN MONTH(GETDATE()) >= 9 THEN YEAR(GETDATE()) ELSE YEAR(GETDATE()) - 1 END
    - CASE WHEN MONTH(g.date_created) >= 9 THEN YEAR(g.date_created) ELSE YEAR(g.date_created) - 1 END
    + 1  AS course_year
FROM study_group g
JOIN department    d  ON d.department_id     = g.department_id
JOIN academic_unit au ON au.academic_unit_id = d.academic_unit_id
WHERE g.date_created <= @date
  AND (g.date_closed IS NULL OR g.date_closed >= @date)
ORDER BY g.group_code;

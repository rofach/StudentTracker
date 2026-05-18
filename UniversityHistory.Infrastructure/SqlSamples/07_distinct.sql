DECLARE @studentId UNIQUEIDENTIFIER = N'00000001-0000-0000-0000-000000000000';
DECLARE @dateFrom  DATE = '2024-01-01'; 
DECLARE @dateTo    DATE = '2026-01-02';

SELECT DISTINCT
    other_e.student_id  AS classmate_student_id,
    s.first_name        AS first_name,
    s.last_name         AS last_name,
    g.group_code        AS group_code
FROM student_group_enrollment mine_e
JOIN student_group_enrollment other_e
    ON other_e.group_id    = mine_e.group_id
   AND other_e.student_id <> @studentId
JOIN student     s ON s.student_id = other_e.student_id
JOIN study_group g ON g.group_id   = other_e.group_id
WHERE mine_e.student_id = @studentId
  AND mine_e.date_from <= ISNULL(other_e.date_to, '9999-12-31')
  AND ISNULL(mine_e.date_to, '9999-12-31') >= other_e.date_from
  AND (@dateFrom IS NULL OR ISNULL(mine_e.date_to,  '9999-12-31') >= @dateFrom)
  AND (@dateTo   IS NULL OR mine_e.date_from        <= @dateTo)
ORDER BY s.last_name, s.first_name;

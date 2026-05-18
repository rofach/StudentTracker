SELECT
    t.transfer_id       AS transfer_id,
    CONCAT(s.last_name, N' ', s.first_name) AS student_name,
    old_g.group_code    AS old_group_code,
    new_g.group_code    AS new_group_code,
    t.transfer_date     AS transfer_date,
    t.reason            AS reason
FROM student_group_transfer t
JOIN student_group_enrollment old_e ON old_e.enrollment_id = t.old_enrollment_id
JOIN student     s    ON s.student_id   = old_e.student_id
JOIN study_group old_g ON old_g.group_id = old_e.group_id
JOIN student_group_enrollment new_e ON new_e.enrollment_id = t.new_enrollment_id
JOIN study_group new_g ON new_g.group_id = new_e.group_id
WHERE s.last_name LIKE N'%Коваль%'
ORDER BY t.transfer_date DESC;
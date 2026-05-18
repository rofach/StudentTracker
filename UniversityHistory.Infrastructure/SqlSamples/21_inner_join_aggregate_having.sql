SELECT
    t.transfer_id AS transfer_id,
    s.student_id AS student_id,
    CONCAT(s.last_name, N' ', s.first_name) AS student_name,
    old_g.group_code AS old_group_code,
    new_g.group_code AS new_group_code,
    t.transfer_date  AS transfer_date,
    COUNT(adi.difference_item_id) AS difference_items_total,
    SUM(CASE WHEN adi.status = N'Pending'   THEN 1 ELSE 0 END)     AS difference_items_pending,
    SUM(CASE WHEN adi.status = N'Completed' THEN 1 ELSE 0 END)     AS difference_items_completed,
    SUM(CASE WHEN adi.status = N'Waived'    THEN 1 ELSE 0 END)     AS difference_items_waived
FROM student_group_transfer t
JOIN student_group_enrollment old_e ON old_e.enrollment_id = t.old_enrollment_id
JOIN student_group_enrollment new_e ON new_e.enrollment_id = t.new_enrollment_id
JOIN student      s                  ON s.student_id        = old_e.student_id
JOIN study_group  old_g              ON old_g.group_id      = old_e.group_id
JOIN study_group  new_g              ON new_g.group_id      = new_e.group_id
LEFT JOIN academic_difference_item adi ON adi.transfer_id   = t.transfer_id
GROUP BY
    t.transfer_id,
    s.student_id, s.last_name, s.first_name,
    old_g.group_code, new_g.group_code,
    t.transfer_date
HAVING SUM(CASE WHEN adi.status = N'Pending' THEN 1 ELSE 0 END) > 0
ORDER BY t.transfer_date DESC, CONCAT(s.last_name, N' ', s.first_name);
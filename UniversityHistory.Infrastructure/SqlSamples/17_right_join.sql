SELECT 
    s.last_name, 
    s.first_name, 
    ce.academic_year_start,
    gr.grade_value
FROM grade_record gr
RIGHT JOIN student_course_enrollment ce ON gr.course_enrollment_id = ce.course_enrollment_id
JOIN student_group_enrollment e ON ce.enrollment_id = e.enrollment_id
JOIN student s ON e.student_id = s.student_id;

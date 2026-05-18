SELECT
    s.student_id,
    s.first_name,
    s.last_name,
    s.patronymic,
    s.birth_date,
    s.email,
    s.phone,
    s.status
FROM student s
WHERE s.last_name              LIKE N'%Ткачук%'
   OR s.first_name             LIKE N'%Марія%'
   OR ISNULL(s.patronymic, N'') LIKE N'%Олегівна%';

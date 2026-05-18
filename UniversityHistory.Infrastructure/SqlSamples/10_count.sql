SELECT
    status,
    COUNT(*) AS StudentCount
FROM Student
GROUP BY status;

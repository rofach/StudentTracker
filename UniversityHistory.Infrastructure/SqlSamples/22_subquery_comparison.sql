SELECT
    pd.plan_discipline_id   AS plan_discipline_id,
    d.discipline_name       AS discipline_name,
    pd.credits              AS credits,
    pd.semester_no          AS semester_no,
    sp.specialty_code       AS specialty_code
FROM plan_disciplines pd
JOIN discipline d ON d.discipline_id = pd.discipline_id
JOIN Study_Plan  sp ON sp.plan_id    = pd.plan_id
WHERE pd.credits > (
    SELECT AVG(credits)
    FROM plan_disciplines
)
ORDER BY pd.credits DESC, d.discipline_name;

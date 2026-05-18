SELECT
    d.discipline_id     AS discipline_id,
    d.discipline_name   AS discipline_name,
    d.description       AS description
FROM discipline d
WHERE d.discipline_id = ANY (
    SELECT pd.discipline_id
    FROM plan_disciplines pd
    WHERE pd.credits > 5
)
ORDER BY d.discipline_name;
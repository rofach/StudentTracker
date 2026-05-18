SELECT
    d.discipline_id                      AS discipline_id,
    d.discipline_name                    AS discipline_name,
    d.description                        AS description,
    COUNT(pd.plan_discipline_id)         AS plan_usage_count
FROM discipline d
LEFT JOIN plan_disciplines pd ON pd.discipline_id = d.discipline_id
GROUP BY d.discipline_id, d.discipline_name, d.description
ORDER BY plan_usage_count DESC, d.discipline_name;

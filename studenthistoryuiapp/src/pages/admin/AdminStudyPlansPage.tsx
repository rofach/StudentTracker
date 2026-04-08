import { useEffect, useState } from "react"
import { getStudyPlans } from "../../api/studyPlansApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { StudyPlanDto } from "../../types/api"
import { formatDate } from "../../utils/format"

type AdminStudyPlansPageProps = {
  navigate: (path: string) => void
}

export function AdminStudyPlansPage({ navigate }: AdminStudyPlansPageProps) {
  const [plans, setPlans] = useState<StudyPlanDto[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    getStudyPlans()
      .then((result) => {
        if (!isActive) {
          return
        }

        setPlans(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити навчальні плани.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [])

  return (
    <div className="page-stack">
      <PageHeader
        title="Навчальні плани"
        description="Керуванння навчальними планами"
        actions={
          <button type="button" onClick={() => navigate("/admin/study-plans/new")}>
            Створити план
          </button>
        }
      />

      <section className="panel">
        <h2>Список навчальних планів</h2>
        {isLoading ? <Spinner label="Завантаження планів..." /> : null}
        {error ? <StatusState tone="error" message={error} /> : null}
        {!isLoading && !error && plans.length === 0 ? (
          <StatusState tone="info" message="Навчальні плани відсутні." />
        ) : null}

        {!isLoading && !error && plans.length > 0 ? (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Назва</th>
                  <th>Код спеціальності</th>
                  <th>Діє з</th>
                  <th>Дії</th>
                </tr>
              </thead>
              <tbody>
                {plans.map((plan) => (
                  <tr key={plan.planId}>
                    <td>{plan.planName ?? "—"}</td>
                    <td>{plan.specialtyCode}</td>
                    <td>{formatDate(plan.validFrom)}</td>
                    <td>
                      <button type="button" onClick={() => navigate(`/admin/study-plans/${plan.planId}`)}>
                        Відкрити
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : null}
      </section>
    </div>
  )
}

import { useEffect, useState } from "react"
import { getStudentDetails } from "../../api/studentsApi"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { StudentDetailDto } from "../../types/api"
import { formatDate, formatNullable, fullName } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type StudentOverviewPageProps = {
  studentId: number
}

export function StudentOverviewPage({ studentId }: StudentOverviewPageProps) {
  const [data, setData] = useState<StudentDetailDto | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true

    setIsLoading(true)
    setError(null)

    getStudentDetails(studentId)
      .then((result) => {
        if (!isActive) return
        setData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити дані студента.")
      })
      .finally(() => {
        if (!isActive) return
        setIsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [studentId])

  if (isLoading) {
    return <Spinner label="Завантаження даних студента..." />
  }

  if (error) {
    return <StatusState tone="error" message={error} />
  }

  if (!data) {
    return <StatusState tone="info" message="Дані студента відсутні." />
  }

  const currentEnrollment = data.enrollments.find((item) => item.dateTo === null) ?? data.enrollments[0]
  const currentPlan = data.plans.find((item) => item.dateTo === null) ?? data.plans[0]

  return (
    <div className="page-stack">
      <section className="panel">
        <h2>Профіль студента</h2>
        <div className="summary-grid">
          <div>
            <strong>ПІБ:</strong> {fullName(data.firstName, data.lastName, data.patronymic)}
          </div>
          <div>
            <strong>Статус:</strong> {formatStudentStatus(data.status)}
          </div>
          <div>
            <strong>Дата народження:</strong> {formatDate(data.birthDate)}
          </div>
          <div>
            <strong>Email:</strong> {formatNullable(data.email)}
          </div>
          <div>
            <strong>Телефон:</strong> {formatNullable(data.phone)}
          </div>
        </div>
      </section>

      <section className="panel">
        <h2>Поточне навчання</h2>
        {currentEnrollment ? (
          <div className="summary-grid">
            <div>
              <strong>Група:</strong> {currentEnrollment.groupCode}
            </div>
            <div>
              <strong>Кафедра:</strong> {currentEnrollment.departmentName}
            </div>
            <div>
              <strong>Підрозділ:</strong> {currentEnrollment.academicUnitName}
            </div>
            <div>
              <strong>Підгрупа:</strong> {formatNullable(currentEnrollment.subgroupName)}
            </div>
          </div>
        ) : (
          <StatusState tone="info" message="Немає активного зарахування в групу." />
        )}

        {currentPlan ? (
          <div className="summary-grid summary-grid--compact">
            <div>
              <strong>Поточний план:</strong> {formatNullable(currentPlan.planName)}
            </div>
            <div>
              <strong>Спеціальність:</strong> {currentPlan.specialtyCode}
            </div>
            <div>
              <strong>З:</strong> {formatDate(currentPlan.dateFrom)}
            </div>
          </div>
        ) : (
          <StatusState tone="info" message="Для студента ще не визначено навчальний план через групу." />
        )}
      </section>
    </div>
  )
}

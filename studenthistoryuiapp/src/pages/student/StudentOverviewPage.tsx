import { useEffect, useMemo, useState } from "react"
import { getStudentDetails } from "../../api/studentsApi"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { EntityId, StudentDetailDto } from "../../types/api"
import { formatDate, formatNullable, fullName } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"
import { formatDifferenceSummary } from "../../utils/difference"

type StudentOverviewPageProps = {
  studentId: EntityId
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
        if (!isActive) {
          return
        }

        setData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити дані студента.")
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
  }, [studentId])

  const currentEnrollment = useMemo(
    () => data?.enrollments.find((item) => item.dateTo === null) ?? data?.enrollments[0] ?? null,
    [data],
  )

  const currentPlan = useMemo(() => data?.plans.find((item) => item.dateTo === null) ?? data?.plans[0] ?? null, [data])

  const openLeave = useMemo(() => data?.leaves.find((item) => item.endDate === null) ?? null, [data])

  if (isLoading) {
    return <Spinner label="Завантаження даних студента..." />
  }

  if (error) {
    return <StatusState tone="error" message={error} />
  }

  if (!data) {
    return <StatusState tone="info" message="Дані студента недоступні." />
  }

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
          <div>
            <strong>Стан навчання:</strong> {data.isOnAcademicLeave ? "Академвідпустка" : "Навчається"}
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
          <StatusState tone="info" message="Немає активного зарахування до групи." />
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
              <strong>Діє з:</strong> {formatDate(currentPlan.dateFrom)}
            </div>
          </div>
        ) : (
          <StatusState tone="info" message="Через групу ще не призначено поточний навчальний план." />
        )}
      </section>

      <section className="panel">
        <h2>Академвідпустка</h2>
        {openLeave ? (
          <div className="summary-grid">
            <div>
              <strong>Початок:</strong> {formatDate(openLeave.startDate)}
            </div>
            <div>
              <strong>Причина:</strong> {formatNullable(openLeave.reason)}
            </div>
            <div>
              <strong>Заплановане завершення:</strong> {formatDate(openLeave.endDate)}
            </div>
          </div>
        ) : (
          <StatusState tone="info" message="Зараз немає відкритої академвідпустки." />
        )}
      </section>

      <section className="panel">
        <h2>Внутрішні переведення</h2>
        {data.internalTransfers.length === 0 ? (
          <StatusState tone="info" message="Внутрішніх переведень ще не було." />
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Дата</th>
                  <th>Маршрут</th>
                  <th>Причина</th>
                  <th>Академрізниця</th>
                </tr>
              </thead>
              <tbody>
                {data.internalTransfers.map((transfer) => (
                  <tr key={transfer.transferId}>
                    <td>{formatDate(transfer.transferDate)}</td>
                    <td>
                      {transfer.oldGroupCode} → {transfer.newGroupCode}
                    </td>
                    <td>{transfer.reason}</td>
                    <td>{formatDifferenceSummary(transfer.differenceItemsPending, transfer.differenceItemsTotal)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </div>
  )
}

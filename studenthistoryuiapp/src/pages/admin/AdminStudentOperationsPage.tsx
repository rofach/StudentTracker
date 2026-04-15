import { useEffect, useMemo, useState } from "react"
import { getStudyPlans } from "../../api/studyPlansApi"
import {
  closeEnrollment,
  createAcademicLeave,
  enrollStudent,
  getSelectableGroups,
  getStudentDetails,
  moveStudentToGroup,
} from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { ActiveGroupDto, StudentDetailDto, StudyPlanDto } from "../../types/api"
import { formatDate, fullName } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type AdminStudentOperationsPageProps = {
  studentId: number
  navigate: (path: string) => void
}

function todayValue(): string {
  return new Date().toISOString().slice(0, 10)
}

export function AdminStudentOperationsPage({ studentId, navigate }: AdminStudentOperationsPageProps) {
  const [student, setStudent] = useState<StudentDetailDto | null>(null)
  const [groups, setGroups] = useState<ActiveGroupDto[]>([])
  const [plans, setPlans] = useState<StudyPlanDto[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  const [enrollGroupId, setEnrollGroupId] = useState<number | "">("")
  const [enrollDate, setEnrollDate] = useState(todayValue())
  const [enrollReasonStart, setEnrollReasonStart] = useState("Вступ")

  const [moveGroupId, setMoveGroupId] = useState<number | "">("")
  const [moveDate, setMoveDate] = useState(todayValue())
  const [moveReasonEnd, setMoveReasonEnd] = useState("Переведення")
  const [moveReasonStart, setMoveReasonStart] = useState("Переведення")

  const [closeDate, setCloseDate] = useState(todayValue())
  const [closeReasonEnd, setCloseReasonEnd] = useState("")

  const [leaveDate, setLeaveDate] = useState(todayValue())
  const [leaveReason, setLeaveReason] = useState("")

  const loadData = async () => {
    const [studentResult, groupsResult, plansResult] = await Promise.all([
      getStudentDetails(studentId),
      getSelectableGroups(),
      getStudyPlans(),
    ])

    setStudent(studentResult)
    setGroups(groupsResult)
    setPlans(plansResult)
  }

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    loadData()
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити операції студента.")
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
    () => student?.enrollments.find((item) => item.dateTo === null) ?? null,
    [student],
  )

  const openPlan = useMemo(() => student?.plans.find((item) => item.dateTo === null) ?? null, [student])

  const availableMoveGroups = useMemo(() => {
    if (!currentEnrollment) {
      return groups
    }

    return groups.filter((group) => group.groupId !== currentEnrollment.groupId)
  }, [groups, currentEnrollment])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      await action()
      await loadData()
      setMessage(successMessage)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося виконати операцію.")
    } finally {
      setIsSaving(false)
    }
  }

  if (isLoading) {
    return <Spinner label="Завантаження сторінки операцій..." />
  }

  if (error && !student) {
    return <StatusState tone="error" message={error} />
  }

  if (!student) {
    return <StatusState tone="info" message="Дані студента відсутні." />
  }

  return (
    <div className="page-stack">
      <PageHeader
        title={fullName(student.firstName, student.lastName, student.patronymic)}
        description="Академічні операції над студентом."
        actions={
          <div className="inline-actions">
            <button type="button" onClick={() => navigate(`/admin/students/${studentId}`)}>
              До картки студента
            </button>
            <button type="button" onClick={() => navigate("/admin/students")}>
              Назад до списку
            </button>
          </div>
        }
      />

      <section className="panel">
        <h2>Поточний стан</h2>
        <div className="summary-grid">
          <div>
            <strong>Статус:</strong> {formatStudentStatus(student.status)}
          </div>
          <div>
            <strong>Активна група:</strong> {currentEnrollment?.groupCode ?? "Немає"}
          </div>
          <div>
            <strong>Дата зарахування:</strong> {formatDate(currentEnrollment?.dateFrom ?? null)}
          </div>
          <div>
            <strong>Поточний план:</strong> {openPlan?.planName ?? "Немає"}
          </div>
        </div>
      </section>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Зарахування до групи</h2>
          <div className="form-grid">
            <label>
              Група
              <select
                value={enrollGroupId}
                onChange={(event) => setEnrollGroupId(event.target.value ? Number(event.target.value) : "")}
              >
                <option value="">Оберіть групу</option>
                {groups.map((group) => (
                  <option key={group.groupId} value={group.groupId}>
                    {group.groupCode} ({group.departmentName})
                  </option>
                ))}
              </select>
            </label>
            <label>
              Дата
              <input type="date" value={enrollDate} onChange={(event) => setEnrollDate(event.target.value)} />
            </label>
            <label>
              Причина початку
              <input
                type="text"
                value={enrollReasonStart}
                onChange={(event) => setEnrollReasonStart(event.target.value)}
              />
            </label>
          </div>
          <button
            type="button"
            disabled={isSaving || enrollGroupId === ""}
            onClick={() =>
              void runAction(
                () =>
                  enrollStudent({
                    studentId,
                    groupId: Number(enrollGroupId),
                    subgroupId: null,
                    dateFrom: enrollDate,
                    reasonStart: enrollReasonStart,
                  }).then(() => undefined),
                "Студента зараховано до групи.",
              )
            }
          >
            {isSaving ? "Виконання..." : "Зарахувати"}
          </button>
        </section>

        <section className="panel">
          <h2>Переведення до іншої групи</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Нова група
                  <select
                    value={moveGroupId}
                    onChange={(event) => setMoveGroupId(event.target.value ? Number(event.target.value) : "")}
                  >
                    <option value="">Оберіть групу</option>
                    {availableMoveGroups.map((group) => (
                      <option key={group.groupId} value={group.groupId}>
                        {group.groupCode} ({group.departmentName})
                      </option>
                    ))}
                  </select>
                </label>
                <label>
                  Дата переведення
                  <input type="date" value={moveDate} onChange={(event) => setMoveDate(event.target.value)} />
                </label>
                <label>
                  Причина завершення
                  <input
                    type="text"
                    value={moveReasonEnd}
                    onChange={(event) => setMoveReasonEnd(event.target.value)}
                  />
                </label>
                <label>
                  Причина початку
                  <input
                    type="text"
                    value={moveReasonStart}
                    onChange={(event) => setMoveReasonStart(event.target.value)}
                  />
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || moveGroupId === ""}
                onClick={() =>
                  void runAction(
                    () =>
                      moveStudentToGroup(studentId, {
                        newGroupId: Number(moveGroupId),
                        newSubgroupId: null,
                        moveDate,
                        reasonEnd: moveReasonEnd,
                        reasonStart: moveReasonStart,
                      }),
                    "Переведення до іншої групи виконано.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Перевести"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Активне зарахування відсутнє. Спочатку зарахуйте студента до групи." />
          )}
        </section>

        <section className="panel">
          <h2>Закриття поточного зарахування</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Дата завершення
                  <input type="date" value={closeDate} onChange={(event) => setCloseDate(event.target.value)} />
                </label>
                <label>
                  Причина завершення
                  <input
                    type="text"
                    value={closeReasonEnd}
                    onChange={(event) => setCloseReasonEnd(event.target.value)}
                  />
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || closeReasonEnd.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () =>
                      closeEnrollment(currentEnrollment.enrollmentId, {
                        dateTo: closeDate,
                        reasonEnd: closeReasonEnd,
                      }),
                    "Поточне зарахування закрито.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Закрити зарахування"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="У студента немає активного зарахування для закриття." />
          )}
        </section>

        <section className="panel">
          <h2>Академвідпустка</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Дата початку
                  <input type="date" value={leaveDate} onChange={(event) => setLeaveDate(event.target.value)} />
                </label>
                <label>
                  Причина
                  <input type="text" value={leaveReason} onChange={(event) => setLeaveReason(event.target.value)} />
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving}
                onClick={() =>
                  void runAction(
                    () =>
                      createAcademicLeave(studentId, {
                        enrollmentId: currentEnrollment.enrollmentId,
                        startDate: leaveDate,
                        reason: leaveReason || null,
                      }),
                    "Академвідпустку оформлено.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Оформити академвідпустку"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Академвідпустка можлива лише для активного зарахування." />
          )}
        </section>
      </div>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Навчальний план</h2>
          <StatusState
            tone="info"
            message={
              currentEnrollment
                ? `План студента визначається через групу ${currentEnrollment.groupCode}. Змінюйте або призначайте план у розділі груп.`
                : "Спочатку зарахуйте студента до групи, щоб для нього визначився навчальний план."
            }
          />
        </section>

        <section className="panel">
          <h2>Доступні плани</h2>
          {plans.length === 0 ? (
            <StatusState tone="info" message="Навчальні плани ще не створені." />
          ) : (
            <div className="table-wrap table-wrap--compact">
              <table>
                <thead>
                  <tr>
                    <th>Назва</th>
                    <th>Спеціальність</th>
                    <th>Діє з</th>
                  </tr>
                </thead>
                <tbody>
                  {plans.map((plan) => (
                    <tr key={plan.planId}>
                      <td>{plan.planName ?? `План ${plan.planId}`}</td>
                      <td>{plan.specialtyCode}</td>
                      <td>{formatDate(plan.validFrom)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </section>
      </div>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}

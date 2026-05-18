import { useEffect, useMemo, useState } from "react"
import { getGroupSubgroups } from "../../api/groupsApi"
import { createInstitution, getInstitutions } from "../../api/institutionsApi"
import {
  assignEnrollmentSubgroup,
  closeEnrollment,
  createAcademicLeave,
  enrollStudent,
  expelStudent,
  getSelectableGroups,
  getStudentDetails,
  graduateStudent,
  moveEnrollmentSubgroup,
  moveStudentToGroup,
  returnStudentFromExternalTransfer,
  transferStudentOut,
  updateAcademicLeave,
} from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useToast } from "../../components/common/ToastCenter"
import type {
  AcademicLeaveDto,
  ActiveGroupDto,
  EntityId,
  InstitutionDto,
  StudentDetailDto,
  SubgroupDto,
} from "../../types/api"
import { formatDate, fullName } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type AdminStudentOperationsPageProps = {
  studentId: EntityId
  navigate: (path: string) => void
}

type SubgroupOption = {
  subgroupId: EntityId
  subgroupName: string
}

function todayValue(): string {
  const now = new Date()
  const year = now.getFullYear()
  const month = String(now.getMonth() + 1).padStart(2, "0")
  const day = String(now.getDate()).padStart(2, "0")
  return `${year}-${month}-${day}`
}

function isLeaveActiveOnDate(leave: AcademicLeaveDto, date: string): boolean {
  return leave.startDate <= date && (!leave.endDate || leave.endDate >= date)
}

function mapSubgroups(items: SubgroupDto[]): SubgroupOption[] {
  return items
    .map((item) => ({ subgroupId: item.subgroupId, subgroupName: item.subgroupName }))
    .sort((left, right) => left.subgroupName.localeCompare(right.subgroupName))
}

function parseAdmissionYear(groupCode: string): number | null {
  const suffix = groupCode.split("-").at(-1)?.trim()
  if (!suffix || !/^\d{2}$/.test(suffix)) {
    return null
  }

  return 2000 + Number(suffix)
}

function canMoveToGroup(currentGroupCode: string | null | undefined, targetGroupCode: string): boolean {
  if (!currentGroupCode) {
    return true
  }

  const currentAdmissionYear = parseAdmissionYear(currentGroupCode)
  const targetAdmissionYear = parseAdmissionYear(targetGroupCode)
  if (currentAdmissionYear === null || targetAdmissionYear === null) {
    return true
  }

  return targetAdmissionYear >= currentAdmissionYear
}

function sortLeaves(items: AcademicLeaveDto[]): AcademicLeaveDto[] {
  return [...items].sort((left, right) => right.startDate.localeCompare(left.startDate))
}

function formatLeaveOption(leave: AcademicLeaveDto): string {
  return leave.endDate
    ? `${formatDate(leave.startDate)} - ${formatDate(leave.endDate)}`
    : `${formatDate(leave.startDate)} - відкрита`
}

export function AdminStudentOperationsPage({ studentId, navigate }: AdminStudentOperationsPageProps) {
  const { pushToast } = useToast()
  const [student, setStudent] = useState<StudentDetailDto | null>(null)
  const [groups, setGroups] = useState<ActiveGroupDto[]>([])
  const [institutions, setInstitutions] = useState<InstitutionDto[]>([])
  const [subgroupOptions, setSubgroupOptions] = useState<SubgroupOption[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const [enrollGroupId, setEnrollGroupId] = useState<EntityId | "">("")
  const [enrollDate, setEnrollDate] = useState(todayValue())
  const [enrollReasonStart, setEnrollReasonStart] = useState("Первинне зарахування")

  const [moveGroupId, setMoveGroupId] = useState<EntityId | "">("")
  const [moveDate, setMoveDate] = useState(todayValue())
  const [moveReasonEnd, setMoveReasonEnd] = useState("Переведення до іншої групи")
  const [moveReasonStart, setMoveReasonStart] = useState("Переведення з іншої групи")

  const [closeDate, setCloseDate] = useState(todayValue())
  const [closeReasonEnd, setCloseReasonEnd] = useState("")

  const [leaveDate, setLeaveDate] = useState(todayValue())
  const [leaveEndDate, setLeaveEndDate] = useState("")
  const [leaveReason, setLeaveReason] = useState("")

  const [selectedLeaveId, setSelectedLeaveId] = useState<EntityId | "">("")
  const [editLeaveStartDate, setEditLeaveStartDate] = useState("")
  const [editLeaveEndDate, setEditLeaveEndDate] = useState("")
  const [editLeaveReason, setEditLeaveReason] = useState("")
  const [editLeaveReturnReason, setEditLeaveReturnReason] = useState("")

  const [assignSubgroupId, setAssignSubgroupId] = useState<EntityId | "">("")
  const [subgroupMoveId, setSubgroupMoveId] = useState<EntityId | "">("")
  const [subgroupMoveDate, setSubgroupMoveDate] = useState(todayValue())
  const [subgroupMoveReason, setSubgroupMoveReason] = useState("Переведення до підгрупи")

  const [expelReason, setExpelReason] = useState("")
  const [graduateReason, setGraduateReason] = useState("Успішне завершення навчання")

  const [transferOutInstitutionId, setTransferOutInstitutionId] = useState<EntityId | "custom" | "">("")
  const [transferOutNewInstitutionName, setTransferOutNewInstitutionName] = useState("")
  const [transferOutDate, setTransferOutDate] = useState(todayValue())
  const [transferOutReasonEnd, setTransferOutReasonEnd] = useState("Переведення до іншого закладу")
  const [transferOutNotes, setTransferOutNotes] = useState("")

  const [returnInstitutionId, setReturnInstitutionId] = useState<EntityId | "custom" | "">("")
  const [returnNewInstitutionName, setReturnNewInstitutionName] = useState("")
  const [returnGroupId, setReturnGroupId] = useState<EntityId | "">("")
  const [returnSubgroupId, setReturnSubgroupId] = useState<EntityId | "">("")
  const [returnDate, setReturnDate] = useState(todayValue())
  const [returnReasonStart, setReturnReasonStart] = useState("Поновлення після переведення")
  const [returnNotes, setReturnNotes] = useState("")

  async function loadData() {
    const [studentResult, groupsResult, institutionsResult] = await Promise.all([
      getStudentDetails(studentId),
      getSelectableGroups(),
      getInstitutions(),
    ])

    setStudent(studentResult)
    setGroups(groupsResult)
    setInstitutions(institutionsResult)
  }

  async function runAction(action: () => Promise<void>, successMessage: string) {
    setIsSaving(true)
    setError(null)

    try {
      await action()
      await loadData()
      pushToast({ tone: "info", title: "Успішно", message: successMessage })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося виконати операцію."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
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

        setError(err instanceof Error ? err.message : "Не вдалося завантажити сторінку операцій студента.")
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
  const openPlan = useMemo(
    () => student?.plans.find((item) => item.dateTo === null) ?? null,
    [student],
  )
  const leaves = useMemo(() => sortLeaves(student?.leaves ?? []), [student?.leaves])
  const activeLeaveToday = useMemo(
    () => leaves.find((item) => isLeaveActiveOnDate(item, todayValue())) ?? null,
    [leaves],
  )
  const selectedLeave = useMemo(
    () => leaves.find((item) => item.leaveId === selectedLeaveId) ?? leaves[0] ?? null,
    [leaves, selectedLeaveId],
  )

  useEffect(() => {
    if (!currentEnrollment) {
      setSubgroupOptions([])
      return
    }

    let isActive = true

    getGroupSubgroups(currentEnrollment.groupId)
      .then((result) => {
        if (!isActive) {
          return
        }

        const options = mapSubgroups(result)
        setSubgroupOptions(options)

        setAssignSubgroupId((current) => {
          if (current && options.some((option) => option.subgroupId === current)) {
            return current
          }

          return options[0]?.subgroupId ?? ""
        })

        setSubgroupMoveId((current) => {
          const filtered = options.filter((option) => option.subgroupId !== currentEnrollment.subgroupId)
          if (current && filtered.some((option) => option.subgroupId === current)) {
            return current
          }

          return filtered[0]?.subgroupId ?? ""
        })
      })
      .catch(() => {
        if (!isActive) {
          return
        }

        setSubgroupOptions([])
      })

    return () => {
      isActive = false
    }
  }, [currentEnrollment?.groupId, currentEnrollment?.subgroupId])

  useEffect(() => {
    if (!leaves.length) {
      setSelectedLeaveId("")
      return
    }

    setSelectedLeaveId((current) => {
      if (current && leaves.some((item) => item.leaveId === current)) {
        return current
      }

      return leaves[0].leaveId
    })
  }, [leaves])

  useEffect(() => {
    if (!selectedLeave) {
      setEditLeaveStartDate("")
      setEditLeaveEndDate("")
      setEditLeaveReason("")
      setEditLeaveReturnReason("")
      return
    }

    setEditLeaveStartDate(selectedLeave.startDate)
    setEditLeaveEndDate(selectedLeave.endDate ?? "")
    setEditLeaveReason(selectedLeave.reason ?? "")
    setEditLeaveReturnReason(selectedLeave.returnReason ?? "")
  }, [selectedLeave])

  const availableMoveGroups = useMemo(() => {
    if (!currentEnrollment) {
      return groups
    }

    return groups.filter(
      (group) =>
        group.groupId !== currentEnrollment.groupId &&
        canMoveToGroup(currentEnrollment.groupCode, group.groupCode),
    )
  }, [groups, currentEnrollment])

  const availableSubgroupMoveTargets = useMemo(
    () => subgroupOptions.filter((option) => option.subgroupId !== currentEnrollment?.subgroupId),
    [subgroupOptions, currentEnrollment?.subgroupId],
  )

  if (isLoading) {
    return <Spinner label="Завантаження сторінки операцій..." />
  }

  if (error && !student) {
    return <StatusState tone="error" message={error} />
  }

  if (!student) {
    return <StatusState tone="info" message="Дані студента недоступні." />
  }

  return (
    <div className="page-stack">
      <PageHeader
        title={fullName(student.firstName, student.lastName, student.patronymic)}
        description="Адміністративні дії."
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

      {error ? <StatusState tone="error" message={error} /> : null}

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
            <strong>Курс:</strong> {currentEnrollment?.courseYear != null ? `${currentEnrollment.courseYear} курс` : "—"}
          </div>
          <div>
            <strong>Підгрупа:</strong> {currentEnrollment?.subgroupName ?? "Немає"}
          </div>
          <div>
            <strong>Дата зарахування:</strong> {formatDate(currentEnrollment?.dateFrom ?? null)}
          </div>
          <div>
            <strong>Поточний план:</strong> {openPlan?.planName ?? "Немає"}
          </div>
          <div>
            <strong>Академвідпустка:</strong> {activeLeaveToday ? `Активна з ${formatDate(activeLeaveToday.startDate)}` : "Немає"}
          </div>
        </div>
      </section>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Зарахування до групи</h2>
          <div className="form-grid">
            <label>
              Група
              <select value={enrollGroupId} onChange={(event) => setEnrollGroupId(event.target.value)}>
                <option value="">Оберіть групу</option>
                {groups.map((group) => (
                  <option key={group.groupId} value={group.groupId}>
                    {group.groupCode} ({group.departmentName})
                  </option>
                ))}
              </select>
            </label>
            <label>
              Дата початку
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
                    groupId: enrollGroupId,
                    subgroupId: null,
                    dateFrom: enrollDate,
                    reasonStart: enrollReasonStart.trim(),
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
                  Цільова група
                  <select value={moveGroupId} onChange={(event) => setMoveGroupId(event.target.value)}>
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
                  Причина завершення поточного зарахування
                  <input type="text" value={moveReasonEnd} onChange={(event) => setMoveReasonEnd(event.target.value)} />
                </label>
                <label>
                  Причина початку нового зарахування
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
                        newGroupId: moveGroupId,
                        newSubgroupId: null,
                        moveDate,
                        reasonEnd: moveReasonEnd.trim(),
                        reasonStart: moveReasonStart.trim(),
                      }),
                    "Студента переведено до нової групи.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Перевести до групи"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Перед переведенням потрібне активне зарахування." />
          )}
        </section>

        <section className="panel">
          <h2>Закриття активного зарахування</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Дата завершення
                  <input type="date" value={closeDate} onChange={(event) => setCloseDate(event.target.value)} />
                </label>
                <label>
                  Причина завершення
                  <input type="text" value={closeReasonEnd} onChange={(event) => setCloseReasonEnd(event.target.value)} />
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
                        reasonEnd: closeReasonEnd.trim(),
                      }),
                    "Активне зарахування закрито.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Закрити зарахування"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Немає активного зарахування для закриття." />
          )}
        </section>

        <section className="panel">
          <h2>Нова академвідпустка</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Дата початку
                  <input type="date" value={leaveDate} onChange={(event) => setLeaveDate(event.target.value)} />
                </label>
                <label>
                  Запланована дата завершення
                  <input type="date" value={leaveEndDate} onChange={(event) => setLeaveEndDate(event.target.value)} />
                </label>
                <label>
                  Причина
                  <input type="text" value={leaveReason} onChange={(event) => setLeaveReason(event.target.value)} />
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || activeLeaveToday !== null || leaveReason.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () =>
                      createAcademicLeave(studentId, {
                        enrollmentId: currentEnrollment.enrollmentId,
                        startDate: leaveDate,
                        endDate: leaveEndDate || null,
                        reason: leaveReason.trim(),
                      }),
                    "Академвідпустку оформлено.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Оформити академвідпустку"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Академвідпустка доступна лише для активного зарахування." />
          )}
        </section>
      </div>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Редагування академвідпустки</h2>
          {leaves.length > 0 ? (
            <>
              <div className="summary-grid summary-grid--compact">
                <div>
                  <strong>Усього записів:</strong> {leaves.length}
                </div>
                <div>
                  <strong>Активна:</strong> {activeLeaveToday ? `так, з ${formatDate(activeLeaveToday.startDate)}` : "ні"}
                </div>
              </div>

              <div className="form-grid">
                <label>
                  Відпустка
                  <select value={selectedLeave?.leaveId ?? ""} onChange={(event) => setSelectedLeaveId(event.target.value)}>
                    {leaves.map((leave) => (
                      <option key={leave.leaveId} value={leave.leaveId}>
                        {formatLeaveOption(leave)}
                      </option>
                    ))}
                  </select>
                </label>
                <label>
                  Дата початку
                  <input
                    type="date"
                    value={editLeaveStartDate}
                    onChange={(event) => setEditLeaveStartDate(event.target.value)}
                  />
                </label>
                <label>
                  Дата завершення
                  <input
                    type="date"
                    value={editLeaveEndDate}
                    onChange={(event) => setEditLeaveEndDate(event.target.value)}
                  />
                </label>
                <label>
                  Причина
                  <input
                    type="text"
                    value={editLeaveReason}
                    onChange={(event) => setEditLeaveReason(event.target.value)}
                  />
                </label>
                <label>
                  Причина повернення
                  <input
                    type="text"
                    value={editLeaveReturnReason}
                    onChange={(event) => setEditLeaveReturnReason(event.target.value)}
                  />
                </label>
              </div>

              <div className="inline-actions">
                <button
                  type="button"
                  disabled={isSaving || !selectedLeave || editLeaveReason.trim().length === 0}
                  onClick={() =>
                    selectedLeave
                      ? void runAction(
                          () =>
                            updateAcademicLeave(selectedLeave.leaveId, {
                              startDate: editLeaveStartDate,
                              endDate: editLeaveEndDate || null,
                              reason: editLeaveReason.trim(),
                              returnReason: editLeaveReturnReason.trim() || null,
                            }),
                          "Академвідпустку оновлено.",
                        )
                      : undefined
                  }
                >
                  {isSaving ? "Виконання..." : "Зберегти зміни"}
                </button>
                <button type="button" disabled={isSaving} onClick={() => setEditLeaveEndDate("")}>
                  Очистити дату завершення
                </button>
              </div>
            </>
          ) : (
            <StatusState tone="info" message="У студента ще немає записів про академвідпустку." />
          )}
        </section>

        <section className="panel">
          <h2>Операції з підгрупою</h2>
          {!currentEnrollment ? (
            <StatusState tone="info" message="Для операцій з підгрупою потрібне активне зарахування." />
          ) : subgroupOptions.length === 0 ? (
            <StatusState tone="info" message="Не вдалося визначити доступні підгрупи для поточної групи." />
          ) : currentEnrollment.subgroupId ? (
            <>
              <div className="summary-grid summary-grid--compact">
                <div>
                  <strong>Поточна підгрупа:</strong> {currentEnrollment.subgroupName ?? "-"}
                </div>
              </div>
              <div className="form-grid">
                <label>
                  Цільова підгрупа
                  <select value={subgroupMoveId} onChange={(event) => setSubgroupMoveId(event.target.value)}>
                    <option value="">Оберіть підгрупу</option>
                    {availableSubgroupMoveTargets.map((option) => (
                      <option key={option.subgroupId} value={option.subgroupId}>
                        {option.subgroupName}
                      </option>
                    ))}
                  </select>
                </label>
                <label>
                  Дата переведення
                  <input
                    type="date"
                    value={subgroupMoveDate}
                    onChange={(event) => setSubgroupMoveDate(event.target.value)}
                  />
                </label>
                <label>
                  Причина
                  <input
                    type="text"
                    value={subgroupMoveReason}
                    onChange={(event) => setSubgroupMoveReason(event.target.value)}
                  />
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || subgroupMoveId === "" || subgroupMoveReason.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () =>
                      moveEnrollmentSubgroup(currentEnrollment.enrollmentId, {
                        newSubgroupId: subgroupMoveId,
                        moveDate: subgroupMoveDate,
                        reason: subgroupMoveReason.trim(),
                      }),
                    "Історію підгрупи оновлено.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Перевести до підгрупи"}
              </button>
            </>
          ) : (
            <>
              <div className="form-grid">
                <label>
                  Підгрупа
                  <select value={assignSubgroupId} onChange={(event) => setAssignSubgroupId(event.target.value)}>
                    <option value="">Оберіть підгрупу</option>
                    {subgroupOptions.map((option) => (
                      <option key={option.subgroupId} value={option.subgroupId}>
                        {option.subgroupName}
                      </option>
                    ))}
                  </select>
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || assignSubgroupId === ""}
                onClick={() =>
                  void runAction(
                    () => assignEnrollmentSubgroup(currentEnrollment.enrollmentId, { subgroupId: assignSubgroupId }),
                    "Підгрупу призначено.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Призначити підгрупу"}
              </button>
            </>
          )}
        </section>
      </div>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Переведення до іншого закладу</h2>
          {currentEnrollment ? (
            <>
              <div className="form-grid">
                <label>
                  Заклад
                  <select
                    value={transferOutInstitutionId}
                    onChange={(event) => setTransferOutInstitutionId(event.target.value)}
                  >
                    <option value="">-- Оберіть заклад --</option>
                    {institutions.map((institution) => (
                      <option key={institution.institutionId} value={institution.institutionId}>
                        {institution.institutionName}
                      </option>
                    ))}
                    <option value="custom">-- Інший (створити новий) --</option>
                  </select>
                </label>
                {transferOutInstitutionId === "custom" && (
                  <label>
                    Назва нового закладу
                    <input
                      type="text"
                      value={transferOutNewInstitutionName}
                      onChange={(event) => setTransferOutNewInstitutionName(event.target.value)}
                      placeholder="Введіть назву"
                    />
                  </label>
                )}
                <label>
                  Дата переведення
                  <input
                    type="date"
                    value={transferOutDate}
                    onChange={(event) => setTransferOutDate(event.target.value)}
                  />
                </label>
                <label>
                  Причина відрахування
                  <input
                    type="text"
                    value={transferOutReasonEnd}
                    onChange={(event) => setTransferOutReasonEnd(event.target.value)}
                  />
                </label>
                <label>
                  Примітка
                  <input
                    type="text"
                    value={transferOutNotes}
                    onChange={(event) => setTransferOutNotes(event.target.value)}
                  />
                </label>
              </div>
              <button
                type="button"
                disabled={
                  isSaving ||
                  transferOutInstitutionId === "" ||
                  (transferOutInstitutionId === "custom" && transferOutNewInstitutionName.trim().length === 0) ||
                  transferOutReasonEnd.trim().length === 0
                }
                onClick={() =>
                  void runAction(
                    async () => {
                      let finalInstitutionId = transferOutInstitutionId
                      if (finalInstitutionId === "custom") {
                        const newInst = await createInstitution(transferOutNewInstitutionName.trim())
                        finalInstitutionId = newInst.institutionId
                      }
                      await transferStudentOut(studentId, {
                        institutionId: finalInstitutionId as string,
                        transferDate: transferOutDate,
                        reasonEnd: transferOutReasonEnd.trim(),
                        notes: transferOutNotes.trim() || null,
                      })
                    },
                    "Студента переведено до іншого закладу.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Перевести до іншого закладу"}
              </button>
            </>
          ) : (
            <StatusState tone="info" message="Перед переведенням потрібне активне зарахування." />
          )}
        </section>

        <section className="panel">
          <h2>Поновлення після переведення</h2>
          {currentEnrollment ? (
            <StatusState tone="info" message="Перед поновленням необхідно закрити поточне зарахування." />
          ) : student.status !== "Expelled" ? (
            <StatusState tone="info" message="Студент не відрахований (поновлювати можна лише відрахованих або переведених студентів)." />
          ) : (
            <>
              <div className="form-grid">
                <label>
                  З якого закладу
                  <select
                    value={returnInstitutionId}
                    onChange={(event) => setReturnInstitutionId(event.target.value)}
                  >
                    <option value="">-- Оберіть заклад --</option>
                    {institutions.map((institution) => (
                      <option key={institution.institutionId} value={institution.institutionId}>
                        {institution.institutionName}
                      </option>
                    ))}
                    <option value="custom">-- Інший (створити новий) --</option>
                  </select>
                </label>
                {returnInstitutionId === "custom" && (
                  <label>
                    Назва нового закладу
                    <input
                      type="text"
                      value={returnNewInstitutionName}
                      onChange={(event) => setReturnNewInstitutionName(event.target.value)}
                      placeholder="Введіть назву"
                    />
                  </label>
                )}
                <label>
                  Цільова група
                  <select value={returnGroupId} onChange={(event) => setReturnGroupId(event.target.value)}>
                    <option value="">Оберіть групу</option>
                    {groups.map((group) => (
                      <option key={group.groupId} value={group.groupId}>
                        {group.groupCode} ({group.departmentName})
                      </option>
                    ))}
                  </select>
                </label>
                <label>
                  Цільова підгрупа
                  <select value={returnSubgroupId} onChange={(event) => setReturnSubgroupId(event.target.value)}>
                    <option value="">Без підгрупи</option>
                    {subgroupOptions.map((option) => (
                      <option key={option.subgroupId} value={option.subgroupId}>
                        {option.subgroupName}
                      </option>
                    ))}
                  </select>
                </label>
                <label>
                  Дата поновлення
                  <input
                    type="date"
                    value={returnDate}
                    onChange={(event) => setReturnDate(event.target.value)}
                  />
                </label>
                <label>
                  Причина поновлення
                  <input
                    type="text"
                    value={returnReasonStart}
                    onChange={(event) => setReturnReasonStart(event.target.value)}
                  />
                </label>
                <label>
                  Примітка
                  <input
                    type="text"
                    value={returnNotes}
                    onChange={(event) => setReturnNotes(event.target.value)}
                  />
                </label>
              </div>
              <button
                type="button"
                disabled={
                  isSaving ||
                  returnInstitutionId === "" ||
                  (returnInstitutionId === "custom" && returnNewInstitutionName.trim().length === 0) ||
                  returnGroupId === "" ||
                  returnReasonStart.trim().length === 0
                }
                onClick={() =>
                  void runAction(
                    async () => {
                      let finalInstitutionId = returnInstitutionId
                      if (finalInstitutionId === "custom") {
                        const newInst = await createInstitution(returnNewInstitutionName.trim())
                        finalInstitutionId = newInst.institutionId
                      }
                      await returnStudentFromExternalTransfer(studentId, {
                        institutionId: finalInstitutionId as string,
                        groupId: returnGroupId,
                        subgroupId: returnSubgroupId || null,
                        dateFrom: returnDate,
                        reasonStart: returnReasonStart.trim(),
                        notes: returnNotes.trim() || null,
                      })
                    },
                    "Студента поновлено після переведення.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Поновити студента"}
              </button>
            </>
          )}
        </section>
      </div>

      <div className="content-grid content-grid--two-columns">
        <section className="panel">
          <h2>Відрахування студента</h2>
          {student.status === "Graduated" ? (
            <StatusState tone="info" message="Випущеного студента не можна відрахувати." />
          ) : student.status === "Expelled" ? (
            <StatusState tone="info" message="Студент вже відрахований. Щоб поновити — зарахуйте до групи." />
          ) : activeLeaveToday ? (
            <StatusState tone="info" message="Перед відрахуванням необхідно закрити активну академвідпустку." />
          ) : (
            <>
              <div className="form-grid">
                <label>
                  Причина відрахування
                  <input
                    id="expel-reason"
                    type="text"
                    value={expelReason}
                    onChange={(e) => setExpelReason(e.target.value)}
                    placeholder="Напр. За власним бажанням"
                  />
                </label>
              </div>
              <button
                id="expel-submit"
                type="button"
                disabled={isSaving || expelReason.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () => expelStudent(studentId, { reason: expelReason.trim() }),
                    "Студента відраховано.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Відрахувати студента"}
              </button>
            </>
          )}
        </section>

        <section className="panel">
          <h2>Випуск студента</h2>
          {student.status === "Graduated" ? (
            <StatusState tone="info" message="Студент вже випущений." />
          ) : student.status !== "Active" ? (
            <StatusState tone="info" message="Випустити можна лише активного студента. Спочатку закрийте академвідпустку або поновіть студента." />
          ) : (
            <>
              <div className="form-grid">
                <label>
                  Причина / примітка
                  <input
                    id="graduate-reason"
                    type="text"
                    value={graduateReason}
                    onChange={(e) => setGraduateReason(e.target.value)}
                  />
                </label>
              </div>
              <button
                id="graduate-submit"
                type="button"
                disabled={isSaving || graduateReason.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () => graduateStudent(studentId, { reason: graduateReason.trim() }),
                    "Студента випущено.",
                  )
                }
              >
                {isSaving ? "Виконання..." : "Випустити студента"}
              </button>
            </>
          )}
        </section>
      </div>
    </div>
  )
}

import { useEffect, useMemo, useState } from "react"
import { assignGroupPlan, changeCurrentGroupPlan, getActiveGroups, getGroupComposition, getGroupPlanHistory } from "../../api/groupsApi"
import { getStudyPlans } from "../../api/studyPlansApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useToast } from "../../components/common/ToastCenter"
import type {
  ActiveGroupDto,
  EntityId,
  GroupCompositionMemberDto,
  GroupPlanAssignmentDto,
  PagedResult,
  StudyPlanDto,
} from "../../types/api"
import { formatDate } from "../../utils/format"

type AdminGroupsPageProps = {
  navigate: (path: string) => void
}

function todayValue(): string {
  return new Date().toISOString().slice(0, 10)
}

export function AdminGroupsPage({ navigate }: AdminGroupsPageProps) {
  const { pushToast } = useToast()
  const [date, setDate] = useState("")
  const [groups, setGroups] = useState<ActiveGroupDto[]>([])
  const [selectedGroupId, setSelectedGroupId] = useState<EntityId | null>(null)
  const [compositionPage, setCompositionPage] = useState(1)
  const [compositionPageSize, setCompositionPageSize] = useState(20)
  const [composition, setComposition] = useState<PagedResult<GroupCompositionMemberDto> | null>(null)
  const [studyPlans, setStudyPlans] = useState<StudyPlanDto[]>([])
  const [planHistory, setPlanHistory] = useState<GroupPlanAssignmentDto[]>([])
  const [selectedPlanId, setSelectedPlanId] = useState<EntityId | "">("")
  const [planDateFrom, setPlanDateFrom] = useState(todayValue())
  const [isGroupsLoading, setIsGroupsLoading] = useState(true)
  const [isCompositionLoading, setIsCompositionLoading] = useState(false)
  const [isPlanHistoryLoading, setIsPlanHistoryLoading] = useState(false)
  const [isPlanSaving, setIsPlanSaving] = useState(false)
  const [hasLoadedGroups, setHasLoadedGroups] = useState(false)
  const [hasLoadedComposition, setHasLoadedComposition] = useState(false)
  const [hasLoadedPlanHistory, setHasLoadedPlanHistory] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setIsGroupsLoading(true)
    setError(null)

    getActiveGroups(date || undefined)
      .then((result) => {
        if (!isActive) {
          return
        }

        setGroups(result)
        setSelectedGroupId((current) => {
          if (result.length === 0) {
            return null
          }

          if (current && result.some((group) => group.groupId === current)) {
            return current
          }

          return result[0].groupId
        })
        setHasLoadedGroups(true)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити групи.")
        setHasLoadedGroups(true)
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsGroupsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [date])

  useEffect(() => {
    let isActive = true

    getStudyPlans()
      .then((result) => {
        if (!isActive) {
          return
        }

        setStudyPlans(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити навчальні плани.")
      })

    return () => {
      isActive = false
    }
  }, [])

  useEffect(() => {
    if (!selectedGroupId) {
      setComposition(null)
      setHasLoadedComposition(false)
      return
    }

    let isActive = true
    setIsCompositionLoading(true)

    getGroupComposition(selectedGroupId, {
      date: date || undefined,
      page: compositionPage,
      pageSize: compositionPageSize,
    })
      .then((result) => {
        if (!isActive) {
          return
        }

        setComposition(result)
        setHasLoadedComposition(true)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити склад групи.")
        setHasLoadedComposition(true)
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsCompositionLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [selectedGroupId, date, compositionPage, compositionPageSize])

  useEffect(() => {
    if (!selectedGroupId) {
      setPlanHistory([])
      setHasLoadedPlanHistory(false)
      return
    }

    let isActive = true
    setIsPlanHistoryLoading(true)

    getGroupPlanHistory(selectedGroupId)
      .then((result) => {
        if (!isActive) {
          return
        }

        setPlanHistory(result)
        setHasLoadedPlanHistory(true)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити історію планів групи.")
        setHasLoadedPlanHistory(true)
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsPlanHistoryLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [selectedGroupId])

  const selectedGroup = useMemo(
    () => groups.find((group) => group.groupId === selectedGroupId) ?? null,
    [groups, selectedGroupId],
  )

  const currentPlan = useMemo(
    () => planHistory.find((assignment) => assignment.dateTo === null) ?? null,
    [planHistory],
  )

  const isGroupsInitialLoading = isGroupsLoading && !hasLoadedGroups
  const isGroupsRefreshing = isGroupsLoading && hasLoadedGroups
  const isCompositionInitialLoading = isCompositionLoading && !hasLoadedComposition
  const isCompositionRefreshing = isCompositionLoading && hasLoadedComposition
  const isPlanHistoryInitialLoading = isPlanHistoryLoading && !hasLoadedPlanHistory
  const isPlanHistoryRefreshing = isPlanHistoryLoading && hasLoadedPlanHistory

  const savePlan = async () => {
    if (!selectedGroupId || selectedPlanId === "") {
      return
    }

    setIsPlanSaving(true)
    setError(null)

    try {
      if (currentPlan) {
        await changeCurrentGroupPlan(selectedGroupId, {
          newPlanId: selectedPlanId,
          newPlanDateFrom: planDateFrom,
        })
        pushToast({ tone: "info", title: "Успішно", message: "Поточний план групи змінено." })
      } else {
        await assignGroupPlan(selectedGroupId, {
          planId: selectedPlanId,
          dateFrom: planDateFrom,
        })
        pushToast({ tone: "info", title: "Успішно", message: "План групі призначено." })
      }

      const updatedHistory = await getGroupPlanHistory(selectedGroupId)
      setPlanHistory(updatedHistory)
      setHasLoadedPlanHistory(true)
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося зберегти план групи."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsPlanSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Групи"
        description="Склад груп і навчальні плани."
        actions={
          <button type="button" onClick={() => navigate("/admin/study-plans")}>
            Навчальні плани
          </button>
        }
      />

      <section className="panel">
        <div className="filters-row">
          <label>
            Дата
            <input
              type="date"
              value={date}
              onChange={(event) => {
                setDate(event.target.value)
                setCompositionPage(1)
              }}
            />
          </label>
        </div>
      </section>

      {isGroupsInitialLoading ? <Spinner label="Завантаження груп..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isGroupsInitialLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <section className="panel">
            <div className="section-heading">
              <h2>Активні групи</h2>
              {isGroupsRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
            </div>

            {groups.length === 0 ? (
              <StatusState tone="info" message="Активні групи не знайдено." />
            ) : (
              <div className="table-wrap table-wrap--compact">
                <table>
                  <thead>
                    <tr>
                      <th>Код</th>
                      <th>Кафедра</th>
                      <th>Підрозділ</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groups.map((group) => (
                      <tr
                        key={group.groupId}
                        className={selectedGroupId === group.groupId ? "row-selected" : ""}
                        onClick={() => setSelectedGroupId(group.groupId)}
                      >
                        <td>{group.groupCode}</td>
                        <td>{group.departmentName}</td>
                        <td>{group.academicUnitName}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </section>

          <section className="panel">
            <h2>Деталі групи</h2>
            {selectedGroup ? (
              <div className="page-stack">
                <div className="summary-grid summary-grid--compact">
                  <div>
                    <strong>Група:</strong> {selectedGroup.groupCode}
                  </div>
                  <div>
                    <strong>Кафедра:</strong> {selectedGroup.departmentName}
                  </div>
                  <div>
                    <strong>Підрозділ:</strong> {selectedGroup.academicUnitName}
                  </div>
                  <div>
                    <strong>Створено:</strong> {formatDate(selectedGroup.dateCreated)}
                  </div>
                </div>

                <section className="panel panel--inner">
                  <div className="section-heading">
                    <h3>Навчальний план групи</h3>
                    {isPlanHistoryRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
                  </div>

                  {isPlanHistoryInitialLoading ? <Spinner label="Завантаження історії планів..." /> : null}

                  {!isPlanHistoryInitialLoading ? (
                    <div className="page-stack">
                      <div className="summary-grid summary-grid--compact">
                        <div>
                          <strong>Поточний план:</strong> {currentPlan?.planName ?? "Не призначено"}
                        </div>
                        <div>
                          <strong>Спеціальність:</strong> {currentPlan?.specialtyCode ?? "—"}
                        </div>
                        <div>
                          <strong>Діє з:</strong> {formatDate(currentPlan?.dateFrom ?? null)}
                        </div>
                      </div>

                      <div className="form-grid">
                        <label>
                          План
                          <select value={selectedPlanId} onChange={(event) => setSelectedPlanId(event.target.value)}>
                            <option value="">Оберіть план</option>
                            {studyPlans.map((plan) => (
                              <option key={plan.planId} value={plan.planId}>
                                {plan.planName ?? `План ${plan.planId}`} ({plan.specialtyCode})
                              </option>
                            ))}
                          </select>
                        </label>
                        <label>
                          {currentPlan ? "Нова дата початку" : "Дата початку"}
                          <input type="date" value={planDateFrom} onChange={(event) => setPlanDateFrom(event.target.value)} />
                        </label>
                      </div>

                      <button type="button" disabled={isPlanSaving || selectedPlanId === ""} onClick={() => void savePlan()}>
                        {isPlanSaving ? "Збереження..." : currentPlan ? "Змінити план групи" : "Призначити план групі"}
                      </button>

                      {planHistory.length > 0 ? (
                        <div className="table-wrap table-wrap--compact">
                          <table>
                            <thead>
                              <tr>
                                <th>План</th>
                                <th>Спеціальність</th>
                                <th>З</th>
                                <th>До</th>
                              </tr>
                            </thead>
                            <tbody>
                              {planHistory.map((assignment) => (
                                <tr key={assignment.groupPlanAssignmentId}>
                                  <td>{assignment.planName ?? `План ${assignment.planId}`}</td>
                                  <td>{assignment.specialtyCode}</td>
                                  <td>{formatDate(assignment.dateFrom)}</td>
                                  <td>{formatDate(assignment.dateTo)}</td>
                                </tr>
                              ))}
                            </tbody>
                          </table>
                        </div>
                      ) : (
                        <StatusState tone="info" message="Для цієї групи план ще не призначено." />
                      )}
                    </div>
                  ) : null}
                </section>

                <section className="panel panel--inner">
                  <div className="section-heading">
                    <h3>Склад групи</h3>
                    {isCompositionRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
                  </div>

                  {isCompositionInitialLoading ? <Spinner label="Завантаження складу..." /> : null}
                  {!isCompositionLoading && composition && composition.items.length === 0 ? (
                    <StatusState tone="info" message="Для обраної групи склад не знайдено." />
                  ) : null}

                  {composition && composition.items.length > 0 ? (
                    <>
                      <div className="table-wrap table-wrap--compact">
                        <table>
                          <thead>
                            <tr>
                              <th>ПІБ</th>
                              <th>Email</th>
                              <th>Підгрупа</th>
                              <th>З</th>
                              <th>До</th>
                            </tr>
                          </thead>
                          <tbody>
                            {composition.items.map((member) => (
                              <tr key={`${member.studentId}-${member.dateFrom}`}>
                                <td>
                                  {member.lastName} {member.firstName}
                                </td>
                                <td>{member.email ?? "—"}</td>
                                <td>{member.subgroupName ?? "—"}</td>
                                <td>{formatDate(member.dateFrom)}</td>
                                <td>{formatDate(member.dateTo)}</td>
                              </tr>
                            ))}
                          </tbody>
                        </table>
                      </div>

                      <PaginationControls
                        page={composition.page}
                        pageSize={composition.pageSize}
                        totalCount={composition.totalCount}
                        onPageChange={setCompositionPage}
                        onPageSizeChange={(nextPageSize) => {
                          setCompositionPageSize(nextPageSize)
                          setCompositionPage(1)
                        }}
                      />
                    </>
                  ) : null}
                </section>
              </div>
            ) : (
              <StatusState tone="info" message="Оберіть групу у списку ліворуч." />
            )}
          </section>
        </div>
      ) : null}
    </div>
  )
}

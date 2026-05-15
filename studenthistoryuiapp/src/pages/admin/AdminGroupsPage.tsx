import { useEffect, useMemo, useState } from "react"
import { createGroup, updateGroup, assignGroupPlan, changeCurrentGroupPlan, getActiveGroups, getGroupComposition, getGroupPlanHistory } from "../../api/groupsApi"
import { getDepartments } from "../../api/structureApi"
import { getStudyPlans } from "../../api/studyPlansApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useToast } from "../../components/common/ToastCenter"
import type {
  ActiveGroupDto,
  DepartmentDto,
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
  const [departments, setDepartments] = useState<DepartmentDto[]>([])
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

  const [showCreateForm, setShowCreateForm] = useState(false)
  const [createCode, setCreateCode] = useState("")
  const [createDeptId, setCreateDeptId] = useState<EntityId | "">("")
  const [createDate, setCreateDate] = useState(todayValue())
  const [isCreating, setIsCreating] = useState(false)

  const [isEditing, setIsEditing] = useState(false)
  const [editCode, setEditCode] = useState("")
  const [isSavingEdit, setIsSavingEdit] = useState(false)

  useEffect(() => {
    let isActive = true
    setIsGroupsLoading(true)
    setError(null)

    getActiveGroups(date || undefined)
      .then((result) => {
        if (!isActive) return
        setGroups(result)
        setSelectedGroupId((current) => {
          if (result.length === 0) return null
          if (current && result.some((g) => g.groupId === current)) return current
          return result[0].groupId
        })
        setHasLoadedGroups(true)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити групи.")
        setHasLoadedGroups(true)
      })
      .finally(() => {
        if (!isActive) return
        setIsGroupsLoading(false)
      })

    return () => { isActive = false }
  }, [date])

  useEffect(() => {
    let isActive = true
    Promise.all([getStudyPlans(), getDepartments()])
      .then(([plans, depts]) => {
        if (!isActive) return
        setStudyPlans(plans)
        setDepartments(depts)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити довідники.")
      })
    return () => { isActive = false }
  }, [])

  useEffect(() => {
    if (!selectedGroupId) {
      setComposition(null)
      setHasLoadedComposition(false)
      return
    }
    let isActive = true
    setIsCompositionLoading(true)
    getGroupComposition(selectedGroupId, { date: date || undefined, page: compositionPage, pageSize: compositionPageSize })
      .then((result) => {
        if (!isActive) return
        setComposition(result)
        setHasLoadedComposition(true)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити склад групи.")
        setHasLoadedComposition(true)
      })
      .finally(() => {
        if (!isActive) return
        setIsCompositionLoading(false)
      })
    return () => { isActive = false }
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
        if (!isActive) return
        setPlanHistory(result)
        setHasLoadedPlanHistory(true)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити історію планів групи.")
        setHasLoadedPlanHistory(true)
      })
      .finally(() => {
        if (!isActive) return
        setIsPlanHistoryLoading(false)
      })
    return () => { isActive = false }
  }, [selectedGroupId])

  const selectedGroup = useMemo(
    () => groups.find((g) => g.groupId === selectedGroupId) ?? null,
    [groups, selectedGroupId],
  )
  const currentPlan = useMemo(
    () => planHistory.find((a) => a.dateTo === null) ?? null,
    [planHistory],
  )

  const isGroupsInitialLoading = isGroupsLoading && !hasLoadedGroups
  const isGroupsRefreshing = isGroupsLoading && hasLoadedGroups
  const isCompositionInitialLoading = isCompositionLoading && !hasLoadedComposition
  const isCompositionRefreshing = isCompositionLoading && hasLoadedComposition
  const isPlanHistoryInitialLoading = isPlanHistoryLoading && !hasLoadedPlanHistory
  const isPlanHistoryRefreshing = isPlanHistoryLoading && hasLoadedPlanHistory

  const savePlan = async () => {
    if (!selectedGroupId || selectedPlanId === "") return
    setIsPlanSaving(true)
    setError(null)
    try {
      if (currentPlan) {
        await changeCurrentGroupPlan(selectedGroupId, { newPlanId: selectedPlanId, newPlanDateFrom: planDateFrom })
        pushToast({ tone: "info", title: "Успішно", message: "Поточний план групи змінено." })
      } else {
        await assignGroupPlan(selectedGroupId, { planId: selectedPlanId, dateFrom: planDateFrom })
        pushToast({ tone: "info", title: "Успішно", message: "План групі призначено." })
      }
      const updatedHistory = await getGroupPlanHistory(selectedGroupId)
      setPlanHistory(updatedHistory)
      setHasLoadedPlanHistory(true)
    } catch (err: unknown) {
      const msg = err instanceof Error ? err.message : "Не вдалося зберегти план групи."
      setError(msg)
      pushToast({ tone: "error", message: msg })
    } finally {
      setIsPlanSaving(false)
    }
  }

  const handleCreate = async () => {
    if (!createCode.trim() || !createDeptId || !createDate) return
    setIsCreating(true)
    setError(null)
    try {
      const created = await createGroup({ groupCode: createCode.trim(), departmentId: createDeptId, dateCreated: createDate })
      pushToast({ tone: "info", title: "Успішно", message: `Групу ${created.groupCode} створено.` })
      setShowCreateForm(false)
      setCreateCode("")
      setCreateDeptId("")
      setCreateDate(todayValue())
      const updated = await getActiveGroups(date || undefined)
      setGroups(updated)
      setSelectedGroupId(created.groupId)
    } catch (err: unknown) {
      const msg = err instanceof Error ? err.message : "Не вдалося створити групу."
      setError(msg)
      pushToast({ tone: "error", message: msg })
    } finally {
      setIsCreating(false)
    }
  }

  const handleUpdate = async () => {
    if (!selectedGroupId || !editCode.trim()) return
    setIsSavingEdit(true)
    setError(null)
    try {
      const updated = await updateGroup(selectedGroupId, { groupCode: editCode.trim() })
      pushToast({ tone: "info", title: "Успішно", message: `Код групи змінено на ${updated.groupCode}.` })
      setIsEditing(false)
      const refreshed = await getActiveGroups(date || undefined)
      setGroups(refreshed)
    } catch (err: unknown) {
      const msg = err instanceof Error ? err.message : "Не вдалося оновити групу."
      setError(msg)
      pushToast({ tone: "error", message: msg })
    } finally {
      setIsSavingEdit(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Групи"
        actions={
          <>
            <button type="button" onClick={() => setShowCreateForm((v) => !v)}>
              {showCreateForm ? "Скасувати" : "Нова група"}
            </button>
            <button type="button" onClick={() => navigate("/admin/study-plans")}>
              Навчальні плани
            </button>
          </>
        }
      />

      {showCreateForm ? (
        <section className="panel">
          <h2>Нова група</h2>
          <div className="form-grid">
            <label>
              Код групи
              <input
                id="create-group-code"
                type="text"
                value={createCode}
                maxLength={20}
                placeholder="Напр. КС-25"
                onChange={(e) => setCreateCode(e.target.value)}
              />
            </label>
            <label>
              Кафедра
              <select id="create-group-dept" value={createDeptId} onChange={(e) => setCreateDeptId(e.target.value)}>
                <option value="">Оберіть кафедру</option>
                {departments.map((d) => (
                  <option key={d.departmentId} value={d.departmentId}>
                    {d.name} ({d.academicUnitName})
                  </option>
                ))}
              </select>
            </label>
            <label>
              Дата створення
              <input
                id="create-group-date"
                type="date"
                value={createDate}
                onChange={(e) => setCreateDate(e.target.value)}
              />
            </label>
          </div>
          <button
            id="create-group-submit"
            type="button"
            disabled={isCreating || !createCode.trim() || !createDeptId || !createDate}
            onClick={() => void handleCreate()}
          >
            {isCreating ? "Збереження..." : "Створити групу"}
          </button>
        </section>
      ) : null}

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
                      <th>Курс</th>
                      <th>Кафедра</th>
                      <th>Підрозділ</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groups.map((group) => (
                      <tr
                        key={group.groupId}
                        className={selectedGroupId === group.groupId ? "row-selected" : ""}
                        onClick={() => { setSelectedGroupId(group.groupId); setIsEditing(false) }}
                      >
                        <td>{group.groupCode}</td>
                        <td>{group.courseYear != null ? `${group.courseYear} курс` : "—"}</td>
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
                    <strong>Група:</strong>{" "}
                    {isEditing ? (
                      <input
                        id="edit-group-code"
                        type="text"
                        value={editCode}
                        maxLength={20}
                        onChange={(e) => setEditCode(e.target.value)}
                        style={{ display: "inline", width: "auto" }}
                      />
                    ) : (
                      selectedGroup.groupCode
                    )}
                  </div>
                  <div><strong>Кафедра:</strong> {selectedGroup.departmentName}</div>
                  <div><strong>Підрозділ:</strong> {selectedGroup.academicUnitName}</div>
                  <div><strong>Створено:</strong> {formatDate(selectedGroup.dateCreated)}</div>
                </div>

                <div style={{ display: "flex", gap: "0.5rem" }}>
                  {isEditing ? (
                    <>
                      <button
                        id="edit-group-save"
                        type="button"
                        disabled={isSavingEdit || !editCode.trim()}
                        onClick={() => void handleUpdate()}
                      >
                        {isSavingEdit ? "Збереження..." : "Зберегти"}
                      </button>
                      <button type="button" onClick={() => setIsEditing(false)}>Скасувати</button>
                    </>
                  ) : (
                    <button
                      id="edit-group-start"
                      type="button"
                      onClick={() => { setEditCode(selectedGroup.groupCode); setIsEditing(true) }}
                    >
                      Редагувати код
                    </button>
                  )}
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
                        <div><strong>Поточний план:</strong> {currentPlan?.planName ?? "Не призначено"}</div>
                        <div><strong>Спеціальність:</strong> {currentPlan?.specialtyCode ?? "—"}</div>
                        <div><strong>Діє з:</strong> {formatDate(currentPlan?.dateFrom ?? null)}</div>
                      </div>
                      <div className="form-grid">
                        <label>
                          План
                          <select value={selectedPlanId} onChange={(e) => setSelectedPlanId(e.target.value)}>
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
                          <input type="date" value={planDateFrom} onChange={(e) => setPlanDateFrom(e.target.value)} />
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
                              {planHistory.map((a) => (
                                <tr key={a.groupPlanAssignmentId}>
                                  <td>{a.planName ?? `План ${a.planId}`}</td>
                                  <td>{a.specialtyCode}</td>
                                  <td>{formatDate(a.dateFrom)}</td>
                                  <td>{formatDate(a.dateTo)}</td>
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
                                <td>{member.lastName} {member.firstName}</td>
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

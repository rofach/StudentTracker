import { useEffect, useMemo, useState } from "react"
import { getDisciplines } from "../../api/disciplinesApi"
import {
  addPlanDiscipline,
  deletePlanDiscipline,
  getPlanDisciplines,
  getStudyPlanById,
  updatePlanDiscipline,
  updateStudyPlan,
} from "../../api/studyPlansApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type {
  AddPlanDisciplineDto,
  DisciplineDto,
  PlanDisciplineDto,
  StudyPlanDto,
  UpdatePlanDisciplineDto,
  UpdateStudyPlanDto,
} from "../../types/api"
import { formatDate } from "../../utils/format"

type AdminStudyPlanDetailPageProps = {
  planId: number
  navigate: (path: string) => void
}

const emptyDisciplineForm: AddPlanDisciplineDto = {
  disciplineId: 0,
  semesterNo: 1,
  controlType: "",
  hours: 0,
  credits: 0,
}

export function AdminStudyPlanDetailPage({ planId, navigate }: AdminStudyPlanDetailPageProps) {
  const [plan, setPlan] = useState<StudyPlanDto | null>(null)
  const [planForm, setPlanForm] = useState<UpdateStudyPlanDto>({
    specialtyCode: "",
    planName: null,
    validFrom: "",
  })
  const [disciplines, setDisciplines] = useState<PlanDisciplineDto[]>([])
  const [allDisciplines, setAllDisciplines] = useState<DisciplineDto[]>([])
  const [selectedDisciplineId, setSelectedDisciplineId] = useState<number | null>(null)
  const [addForm, setAddForm] = useState<AddPlanDisciplineDto>(emptyDisciplineForm)
  const [editForm, setEditForm] = useState<UpdatePlanDisciplineDto>({
    semesterNo: 1,
    controlType: "",
    hours: 0,
    credits: 0,
  })
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  const loadData = async () => {
    const [planResult, planDisciplinesResult, allDisciplinesResult] = await Promise.all([
      getStudyPlanById(planId),
      getPlanDisciplines(planId),
      getDisciplines(),
    ])

    setPlan(planResult)
    setPlanForm({
      specialtyCode: planResult.specialtyCode,
      planName: planResult.planName,
      validFrom: planResult.validFrom,
    })
    setDisciplines(planDisciplinesResult)
    setAllDisciplines(allDisciplinesResult)

    if (planDisciplinesResult.length > 0 && !selectedDisciplineId) {
      setSelectedDisciplineId(planDisciplinesResult[0].disciplineId)
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

        setError(err instanceof Error ? err.message : "Не вдалося завантажити навчальний план.")
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
  }, [planId])

  const selectedDiscipline = useMemo(
    () => disciplines.find((item) => item.disciplineId === selectedDisciplineId) ?? null,
    [disciplines, selectedDisciplineId],
  )

  const availableDisciplines = useMemo(() => {
    const usedIds = new Set(disciplines.map((item) => item.disciplineId))
    return allDisciplines.filter((item) => !usedIds.has(item.disciplineId))
  }, [allDisciplines, disciplines])

  useEffect(() => {
    if (availableDisciplines.length > 0 && addForm.disciplineId === 0) {
      setAddForm((prev) => ({ ...prev, disciplineId: availableDisciplines[0].disciplineId }))
    }
  }, [availableDisciplines, addForm.disciplineId])

  useEffect(() => {
    if (!selectedDiscipline) {
      return
    }

    setEditForm({
      semesterNo: selectedDiscipline.semesterNo,
      controlType: selectedDiscipline.controlType,
      hours: selectedDiscipline.hours,
      credits: selectedDiscipline.credits,
    })
  }, [selectedDiscipline])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      await action()
      await loadData()
      setMessage(successMessage)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося виконати операцію з планом.")
    } finally {
      setIsSaving(false)
    }
  }

  if (isLoading) {
    return <Spinner label="Завантаження навчального плану..." />
  }

  if (error && !plan) {
    return <StatusState tone="error" message={error} />
  }

  if (!plan) {
    return <StatusState tone="info" message="Навчальний план не знайдено." />
  }

  return (
    <div className="page-stack">
      <PageHeader
        title={plan.planName ?? `План ${plan.planId}`}
        description={`Спеціальність ${plan.specialtyCode}. Діє з ${formatDate(plan.validFrom)}.`}
        actions={
          <button type="button" onClick={() => navigate("/admin/study-plans")}>
            Назад до списку
          </button>
        }
      />

      <section className="panel">
        <h2>Шапка плану</h2>
        <div className="form-grid">
          <label>
            Код спеціальності
            <input
              type="text"
              value={planForm.specialtyCode}
              onChange={(event) => setPlanForm((prev) => ({ ...prev, specialtyCode: event.target.value }))}
            />
          </label>
          <label>
            Назва плану
            <input
              type="text"
              value={planForm.planName ?? ""}
              onChange={(event) => setPlanForm((prev) => ({ ...prev, planName: event.target.value || null }))}
            />
          </label>
          <label>
            Діє з
            <input
              type="date"
              value={planForm.validFrom}
              onChange={(event) => setPlanForm((prev) => ({ ...prev, validFrom: event.target.value }))}
            />
          </label>
        </div>

        <button
          type="button"
          disabled={isSaving}
          onClick={() =>
            void runAction(() => updateStudyPlan(planId, planForm).then(() => undefined), "Шапку навчального плану оновлено.")
          }
        >
          {isSaving ? "Збереження..." : "Оновити шапку плану"}
        </button>
      </section>

      <section className="panel">
        <h2>Додати предмет до плану</h2>
        {availableDisciplines.length === 0 ? (
          <StatusState tone="info" message="Усі доступні предмети вже додані до цього плану." />
        ) : (
          <>
            <div className="form-grid">
              <label>
                Предмет
                <select
                  value={addForm.disciplineId}
                  onChange={(event) =>
                    setAddForm((prev) => ({ ...prev, disciplineId: Number(event.target.value) }))
                  }
                >
                  {availableDisciplines.map((discipline) => (
                    <option key={discipline.disciplineId} value={discipline.disciplineId}>
                      {discipline.disciplineName}
                    </option>
                  ))}
                </select>
              </label>
              <label>
                Семестр
                <input
                  type="number"
                  min={1}
                  value={addForm.semesterNo}
                  onChange={(event) =>
                    setAddForm((prev) => ({ ...prev, semesterNo: Number(event.target.value) || 1 }))
                  }
                />
              </label>
              <label>
                Контроль
                <input
                  type="text"
                  value={addForm.controlType}
                  onChange={(event) => setAddForm((prev) => ({ ...prev, controlType: event.target.value }))}
                />
              </label>
              <label>
                Години
                <input
                  type="number"
                  min={0}
                  value={addForm.hours}
                  onChange={(event) => setAddForm((prev) => ({ ...prev, hours: Number(event.target.value) || 0 }))}
                />
              </label>
              <label>
                Кредити
                <input
                  type="number"
                  min={0}
                  step="0.5"
                  value={addForm.credits}
                  onChange={(event) => setAddForm((prev) => ({ ...prev, credits: Number(event.target.value) || 0 }))}
                />
              </label>
            </div>

            <button
              type="button"
              disabled={isSaving || addForm.disciplineId === 0}
              onClick={() =>
                void runAction(
                  () => addPlanDiscipline(planId, addForm).then(() => undefined),
                  "Предмет додано до навчального плану.",
                )
              }
            >
              {isSaving ? "Збереження..." : "Додати предмет"}
            </button>
          </>
        )}
      </section>

      <section className="panel">
        <h2>Предмети плану</h2>
        {disciplines.length === 0 ? (
          <StatusState tone="info" message="Для цього плану поки не додано жодного предмета." />
        ) : (
          <div className="content-grid content-grid--two-columns">
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>Назва</th>
                    <th>Семестр</th>
                    <th>Контроль</th>
                    <th>Години</th>
                    <th>Кредити</th>
                  </tr>
                </thead>
                <tbody>
                  {disciplines.map((item) => (
                    <tr
                      key={`${item.planId}-${item.disciplineId}`}
                      className={selectedDisciplineId === item.disciplineId ? "row-selected" : ""}
                      onClick={() => setSelectedDisciplineId(item.disciplineId)}
                    >
                      <td>{item.disciplineName}</td>
                      <td>{item.semesterNo}</td>
                      <td>{item.controlType}</td>
                      <td>{item.hours}</td>
                      <td>{item.credits}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className="detail-column">
              {selectedDiscipline ? (
                <section className="panel panel--inner">
                  <h3>{selectedDiscipline.disciplineName}</h3>
                  <div className="form-grid">
                    <label>
                      Семестр
                      <input
                        type="number"
                        min={1}
                        value={editForm.semesterNo}
                        onChange={(event) =>
                          setEditForm((prev) => ({ ...prev, semesterNo: Number(event.target.value) || 1 }))
                        }
                      />
                    </label>
                    <label>
                      Контроль
                      <input
                        type="text"
                        value={editForm.controlType}
                        onChange={(event) => setEditForm((prev) => ({ ...prev, controlType: event.target.value }))}
                      />
                    </label>
                    <label>
                      Години
                      <input
                        type="number"
                        min={0}
                        value={editForm.hours}
                        onChange={(event) => setEditForm((prev) => ({ ...prev, hours: Number(event.target.value) || 0 }))}
                      />
                    </label>
                    <label>
                      Кредити
                      <input
                        type="number"
                        min={0}
                        step="0.5"
                        value={editForm.credits}
                        onChange={(event) =>
                          setEditForm((prev) => ({ ...prev, credits: Number(event.target.value) || 0 }))
                        }
                      />
                    </label>
                  </div>

                  <div className="inline-actions">
                    <button
                      type="button"
                      disabled={isSaving}
                      onClick={() =>
                        void runAction(
                          () =>
                            updatePlanDiscipline(planId, selectedDiscipline.disciplineId, editForm).then(() => undefined),
                          "Параметри предмета оновлено.",
                        )
                      }
                    >
                      {isSaving ? "Збереження..." : "Оновити предмет"}
                    </button>
                    <button
                      type="button"
                      disabled={isSaving}
                      onClick={() =>
                        void runAction(
                          () => deletePlanDiscipline(planId, selectedDiscipline.disciplineId),
                          "Предмет видалено з плану.",
                        )
                      }
                    >
                      {isSaving ? "Видалення..." : "Видалити предмет"}
                    </button>
                  </div>
                </section>
              ) : (
                <StatusState tone="info" message="Оберіть предмет у таблиці, щоб змінити його параметри." />
              )}
            </div>
          </div>
        )}
      </section>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}

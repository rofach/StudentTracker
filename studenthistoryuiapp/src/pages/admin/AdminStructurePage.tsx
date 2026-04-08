import { useEffect, useMemo, useState } from "react"
import {
  createAcademicUnit,
  createDepartment,
  getAcademicUnits,
  getDepartments,
  updateAcademicUnit,
  updateDepartment,
} from "../../api/structureApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { AcademicUnitDto, DepartmentDto } from "../../types/api"

export function AdminStructurePage() {
  const [units, setUnits] = useState<AcademicUnitDto[]>([])
  const [departments, setDepartments] = useState<DepartmentDto[]>([])
  const [selectedUnitId, setSelectedUnitId] = useState<number | null>(null)
  const [selectedDepartmentId, setSelectedDepartmentId] = useState<number | null>(null)
  const [newUnitName, setNewUnitName] = useState("")
  const [newUnitType, setNewUnitType] = useState("Faculty")
  const [newDepartmentName, setNewDepartmentName] = useState("")
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)

  const [editUnitName, setEditUnitName] = useState("")
  const [editUnitType, setEditUnitType] = useState("Faculty")
  const [editDepartmentName, setEditDepartmentName] = useState("")

  const loadData = async () => {
    const [unitsResult, departmentsResult] = await Promise.all([getAcademicUnits(), getDepartments()])
    setUnits(unitsResult)
    setDepartments(departmentsResult)

    if (unitsResult.length > 0 && !selectedUnitId) {
      setSelectedUnitId(unitsResult[0].academicUnitId)
    }

    if (departmentsResult.length > 0 && !selectedDepartmentId) {
      setSelectedDepartmentId(departmentsResult[0].departmentId)
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

        setError(err instanceof Error ? err.message : "Не вдалося завантажити структуру.")
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

  const selectedUnit = useMemo(
    () => units.find((unit) => unit.academicUnitId === selectedUnitId) ?? null,
    [units, selectedUnitId],
  )

  const unitDepartments = useMemo(() => {
    if (!selectedUnitId) {
      return departments
    }

    return departments.filter((department) => department.academicUnitId === selectedUnitId)
  }, [departments, selectedUnitId])

  const selectedDepartment = useMemo(
    () => departments.find((department) => department.departmentId === selectedDepartmentId) ?? null,
    [departments, selectedDepartmentId],
  )

  useEffect(() => {
    if (selectedUnit) {
      setEditUnitName(selectedUnit.name)
      setEditUnitType(selectedUnit.type)
    }
  }, [selectedUnit])

  useEffect(() => {
    if (selectedDepartment) {
      setEditDepartmentName(selectedDepartment.name)
    }
  }, [selectedDepartment])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setError(null)
    setMessage(null)

    try {
      await action()
      await loadData()
      setMessage(successMessage)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося виконати дію зі структурою.")
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Структура"
        description="Підрозділи та кафедри"
      />

      {isLoading ? <Spinner label="Завантаження структури..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <div className="page-stack">
            <section className="panel">
              <h2>Новий підрозділ</h2>
              <div className="form-grid">
                <label>
                  Назва
                  <input type="text" value={newUnitName} onChange={(event) => setNewUnitName(event.target.value)} />
                </label>
                <label>
                  Тип
                  <select value={newUnitType} onChange={(event) => setNewUnitType(event.target.value)}>
                    <option value="Faculty">Факультет</option>
                    <option value="Institute">Інститут</option>
                  </select>
                </label>
              </div>
              <button
                type="button"
                disabled={isSaving || newUnitName.trim().length === 0}
                onClick={() =>
                  void runAction(
                    () => createAcademicUnit({ name: newUnitName.trim(), type: newUnitType }).then(() => setNewUnitName("")),
                    "Підрозділ створено.",
                  )
                }
              >
                {isSaving ? "Збереження..." : "Створити підрозділ"}
              </button>
            </section>

            <section className="panel">
              <h2>Підрозділи</h2>
              {units.length === 0 ? (
                <StatusState tone="info" message="Підрозділи відсутні." />
              ) : (
                <div className="table-wrap table-wrap--compact">
                  <table>
                    <thead>
                      <tr>
                        <th>Назва</th>
                        <th>Тип</th>
                      </tr>
                    </thead>
                    <tbody>
                      {units.map((unit) => (
                        <tr
                          key={unit.academicUnitId}
                          className={selectedUnitId === unit.academicUnitId ? "row-selected" : ""}
                          onClick={() => setSelectedUnitId(unit.academicUnitId)}
                        >
                          <td>{unit.name}</td>
                          <td>{unit.type}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              )}
            </section>
          </div>

          <div className="page-stack">
            <section className="panel">
              <h2>Деталі підрозділу</h2>
              {selectedUnit ? (
                <>
                  <div className="form-grid">
                    <label>
                      Назва
                      <input type="text" value={editUnitName} onChange={(event) => setEditUnitName(event.target.value)} />
                    </label>
                    <label>
                      Тип
                      <select value={editUnitType} onChange={(event) => setEditUnitType(event.target.value)}>
                        <option value="Faculty">Факультет</option>
                        <option value="Institute">Інститут</option>
                      </select>
                    </label>
                  </div>
                  <button
                    type="button"
                    disabled={isSaving}
                    onClick={() =>
                      void runAction(
                        () =>
                          updateAcademicUnit(selectedUnit.academicUnitId, {
                            name: editUnitName.trim(),
                            type: editUnitType,
                          }).then(() => undefined),
                        "Підрозділ оновлено.",
                      )
                    }
                  >
                    {isSaving ? "Оновлення..." : "Оновити підрозділ"}
                  </button>
                </>
              ) : (
                <StatusState tone="info" message="Оберіть підрозділ у списку." />
              )}
            </section>

            <section className="panel">
              <h2>Кафедри підрозділу</h2>
              {selectedUnit ? (
                <>
                  <div className="form-grid">
                    <label>
                      Нова кафедра
                      <input
                        type="text"
                        value={newDepartmentName}
                        onChange={(event) => setNewDepartmentName(event.target.value)}
                      />
                    </label>
                  </div>
                  <button
                    type="button"
                    disabled={isSaving || newDepartmentName.trim().length === 0}
                    onClick={() =>
                      void runAction(
                        () =>
                          createDepartment({
                            academicUnitId: selectedUnit.academicUnitId,
                            name: newDepartmentName.trim(),
                          }).then(() => setNewDepartmentName("")),
                        "Кафедру створено.",
                      )
                    }
                  >
                    {isSaving ? "Збереження..." : "Створити кафедру"}
                  </button>

                  {unitDepartments.length === 0 ? (
                    <StatusState tone="info" message="Для цього підрозділу кафедри відсутні." />
                  ) : (
                    <div className="content-grid content-grid--two-columns content-grid--compact">
                      <div className="table-wrap table-wrap--compact">
                        <table>
                          <thead>
                            <tr>
                              <th>Назва кафедри</th>
                            </tr>
                          </thead>
                          <tbody>
                            {unitDepartments.map((department) => (
                              <tr
                                key={department.departmentId}
                                className={selectedDepartmentId === department.departmentId ? "row-selected" : ""}
                                onClick={() => setSelectedDepartmentId(department.departmentId)}
                              >
                                <td>{department.name}</td>
                              </tr>
                            ))}
                          </tbody>
                        </table>
                      </div>

                      <div className="detail-column">
                        {selectedDepartment ? (
                          <section className="panel panel--inner">
                            <h3>Редагування кафедри</h3>
                            <div className="form-grid">
                              <label>
                                Назва
                                <input
                                  type="text"
                                  value={editDepartmentName}
                                  onChange={(event) => setEditDepartmentName(event.target.value)}
                                />
                              </label>
                            </div>
                            <button
                              type="button"
                              disabled={isSaving}
                              onClick={() =>
                                void runAction(
                                  () =>
                                    updateDepartment(selectedDepartment.departmentId, {
                                      name: editDepartmentName.trim(),
                                    }).then(() => undefined),
                                  "Кафедру оновлено.",
                                )
                              }
                            >
                              {isSaving ? "Оновлення..." : "Оновити кафедру"}
                            </button>
                          </section>
                        ) : (
                          <StatusState tone="info" message="Оберіть кафедру у списку." />
                        )}
                      </div>
                    </div>
                  )}
                </>
              ) : (
                <StatusState tone="info" message="Спочатку оберіть підрозділ." />
              )}
            </section>
          </div>
        </div>
      ) : null}

      {message ? <StatusState tone="info" message={message} /> : null}
    </div>
  )
}

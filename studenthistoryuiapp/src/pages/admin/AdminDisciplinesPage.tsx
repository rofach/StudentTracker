import { useEffect, useMemo, useState } from "react"
import { createDiscipline, deleteDiscipline, getDisciplines, updateDiscipline } from "../../api/disciplinesApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { DisciplineDto, EntityId } from "../../types/api"

export function AdminDisciplinesPage() {
  const [items, setItems] = useState<DisciplineDto[]>([])
  const [selectedId, setSelectedId] = useState<EntityId | null>(null)
  const [newName, setNewName] = useState("")
  const [editName, setEditName] = useState("")
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)

  const loadItems = async () => {
    const result = await getDisciplines()
    setItems(result)
    if (result.length > 0 && !selectedId) {
      setSelectedId(result[0].disciplineId)
    }
  }

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    loadItems()
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити предмети.")
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

  const selectedDiscipline = useMemo(
    () => items.find((item) => item.disciplineId === selectedId) ?? null,
    [items, selectedId],
  )

  useEffect(() => {
    if (selectedDiscipline) {
      setEditName(selectedDiscipline.disciplineName)
    }
  }, [selectedDiscipline])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setError(null)
    setMessage(null)

    try {
      await action()
      await loadItems()
      setMessage(successMessage)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося виконати дію з предметом.")
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader title="Предмети" description="Перелік та редагування предметів" />

      <section className="panel">
        <h2>Новий предмет</h2>
        <div className="filters-row">
          <label>
            Назва
            <input type="text" value={newName} onChange={(event) => setNewName(event.target.value)} />
          </label>
          <button
            type="button"
            disabled={isSaving || newName.trim().length === 0}
            onClick={() =>
              void runAction(
                () => createDiscipline({ disciplineName: newName.trim() }).then(() => setNewName("")),
                "Предмет створено.",
              )
            }
          >
            {isSaving ? "Збереження..." : "Створити"}
          </button>
        </div>
      </section>

      {isLoading ? <Spinner label="Завантаження предметів..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <section className="panel">
            <h2>Список предметів</h2>
            {items.length === 0 ? (
              <StatusState tone="info" message="Предмети відсутні." />
            ) : (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>ID</th>
                      <th>Назва</th>
                    </tr>
                  </thead>
                  <tbody>
                    {items.map((item) => (
                      <tr
                        key={item.disciplineId}
                        className={selectedId === item.disciplineId ? "row-selected" : ""}
                        onClick={() => setSelectedId(item.disciplineId)}
                      >
                        <td>{item.disciplineId}</td>
                        <td>{item.disciplineName}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </section>

          <section className="panel">
            <h2>Деталі предмета</h2>
            {selectedDiscipline ? (
              <>
                <div className="form-grid">
                  <label>
                    Назва
                    <input type="text" value={editName} onChange={(event) => setEditName(event.target.value)} />
                  </label>
                </div>

                <div className="inline-actions">
                  <button
                    type="button"
                    disabled={isSaving || editName.trim().length === 0}
                    onClick={() =>
                      void runAction(
                        () =>
                          updateDiscipline(selectedDiscipline.disciplineId, {
                            disciplineName: editName.trim(),
                          }).then(() => undefined),
                        "Предмет оновлено.",
                      )
                    }
                  >
                    {isSaving ? "Оновлення..." : "Оновити"}
                  </button>
                  <button
                    type="button"
                    disabled={isSaving}
                    onClick={() =>
                      void runAction(
                        () =>
                          deleteDiscipline(selectedDiscipline.disciplineId).then(() => {
                            setSelectedId(null)
                            setEditName("")
                          }),
                        "Предмет видалено.",
                      )
                    }
                  >
                    {isSaving ? "Видалення..." : "Видалити"}
                  </button>
                </div>
              </>
            ) : (
              <StatusState tone="info" message="Оберіть предмет у списку ліворуч." />
            )}
          </section>
        </div>
      ) : null}

      {message ? <StatusState tone="info" message={message} /> : null}
    </div>
  )
}

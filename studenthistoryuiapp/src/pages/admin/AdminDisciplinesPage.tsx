import { useEffect, useMemo, useState } from "react"
import { createDiscipline, deleteDiscipline, searchDisciplines, updateDiscipline } from "../../api/disciplinesApi"
import { useToast } from "../../components/common/ToastCenter"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useDebouncedValue } from "../../hooks/useDebouncedValue"
import type { DisciplineSearchItemDto, EntityId, PagedResult } from "../../types/api"

const DEFAULT_PAGE_SIZE = 20

export function AdminDisciplinesPage() {
  const { pushToast } = useToast()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE)
  const [nameFilter, setNameFilter] = useState("")
  const debouncedNameFilter = useDebouncedValue(nameFilter)
  const [data, setData] = useState<PagedResult<DisciplineSearchItemDto> | null>(null)
  const [selectedId, setSelectedId] = useState<EntityId | null>(null)
  const [newName, setNewName] = useState("")
  const [newDescription, setNewDescription] = useState("")
  const [editName, setEditName] = useState("")
  const [editDescription, setEditDescription] = useState("")
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const loadItems = async () => {
    const result = await searchDisciplines({
      name: debouncedNameFilter.trim() || undefined,
      page,
      pageSize,
    })

    setData(result)
    setSelectedId((current) => {
      if (current && result.items.some((item) => item.disciplineId === current)) {
        return current
      }

      return result.items[0]?.disciplineId ?? null
    })
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
  }, [page, pageSize, debouncedNameFilter])

  useEffect(() => {
    setPage(1)
  }, [debouncedNameFilter])

  const items = useMemo(() => data?.items ?? [], [data])
  const hasLoadedData = data !== null
  const isInitialLoading = isLoading && !hasLoadedData
  const isRefreshing = isLoading && hasLoadedData
  const selectedDiscipline = useMemo(
    () => items.find((item) => item.disciplineId === selectedId) ?? null,
    [items, selectedId],
  )

  useEffect(() => {
    if (selectedDiscipline) {
      setEditName(selectedDiscipline.disciplineName)
      setEditDescription(selectedDiscipline.description ?? "")
    } else {
      setEditName("")
      setEditDescription("")
    }
  }, [selectedDiscipline])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setError(null)

    try {
      await action()
      await loadItems()
      pushToast({ tone: "info", title: "Успішно", message: successMessage })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося виконати дію з предметом."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader title="Предмети" description="Пошук і керування переліком дисциплін." />

      <section className="panel">
        <h2>Новий предмет</h2>
        <div className="form-grid">
          <label>
            Назва
            <input type="text" value={newName} onChange={(event) => setNewName(event.target.value)} />
          </label>
          <label className="summary-grid__full">
            Опис
            <textarea
              rows={4}
              value={newDescription}
              onChange={(event) => setNewDescription(event.target.value)}
              placeholder="Коротко опишіть зміст дисципліни"
            />
          </label>
        </div>
        <button
          type="button"
          disabled={isSaving || newName.trim().length === 0}
          onClick={() =>
            void runAction(
              () =>
                createDiscipline({
                  disciplineName: newName.trim(),
                  description: newDescription.trim() || null,
                }).then(() => {
                  setNewName("")
                  setNewDescription("")
                }),
              "Предмет створено.",
            )
          }
        >
          {isSaving ? "Збереження..." : "Створити"}
        </button>
      </section>

      <section className="panel">
        <h2>Пошук предметів</h2>
        <div className="filters-row">
          <label>
            Назва
            <input
              type="text"
              value={nameFilter}
              onChange={(event) => setNameFilter(event.target.value)}
              placeholder="Пошук за назвою"
            />
          </label>
        </div>
      </section>

      {isInitialLoading ? <Spinner label="Завантаження предметів..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isInitialLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <section className="panel">
            <div className="section-heading">
              <h2>Список предметів</h2>
              {isRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
            </div>

            {items.length === 0 ? (
              <StatusState tone="info" message="Предметів не знайдено." />
            ) : (
              <>
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>Назва</th>
                        <th>Опис</th>
                        <th>Використань у планах</th>
                      </tr>
                    </thead>
                    <tbody>
                      {items.map((item) => (
                        <tr
                          key={item.disciplineId}
                          className={selectedId === item.disciplineId ? "row-selected" : ""}
                          onClick={() => setSelectedId(item.disciplineId)}
                        >
                          <td>{item.disciplineName}</td>
                          <td>{item.description?.trim() || "—"}</td>
                          <td>{item.planUsageCount}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>

                <PaginationControls
                  page={page}
                  pageSize={pageSize}
                  totalCount={data?.totalCount ?? 0}
                  onPageChange={setPage}
                  onPageSizeChange={(nextPageSize) => {
                    setPageSize(nextPageSize)
                    setPage(1)
                  }}
                />
              </>
            )}
          </section>

          <section className="panel">
            <h2>Деталі предмета</h2>
            {selectedDiscipline ? (
              <>
                <div className="summary-grid summary-grid--compact">
                  <div>
                    <strong>Використань у планах:</strong> {selectedDiscipline.planUsageCount}
                  </div>
                </div>

                <div className="form-grid">
                  <label>
                    Назва
                    <input type="text" value={editName} onChange={(event) => setEditName(event.target.value)} />
                  </label>
                  <label className="summary-grid__full">
                    Опис
                    <textarea
                      rows={6}
                      value={editDescription}
                      onChange={(event) => setEditDescription(event.target.value)}
                      placeholder="Коротко опишіть зміст дисципліни"
                    />
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
                            description: editDescription.trim() || null,
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
                            setEditDescription("")
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
              <StatusState tone="info" message="Оберіть предмет у списку." />
            )}
          </section>
        </div>
      ) : null}
    </div>
  )
}
